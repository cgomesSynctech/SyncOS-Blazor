using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using info;
using Microsoft.EntityFrameworkCore;

namespace syncos_Blazor.Services
{
    /// <summary>
    /// Serviço responsável por controlar a rotina diária de atualização
    /// de parcelas vencidas no banco de dados, garantindo execução única por dia.
    /// </summary>
    public class ParcelaStatusService
    {
        private readonly IDbContextFactory<APPDbContext> _dbFactory;
        private static DateTime? _ultimaExecucaoEmMemoria;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);
        private const string ChaveParametro = "UltimaVerificacaoParcelasVencidas";

        public ParcelaStatusService(IDbContextFactory<APPDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        /// <summary>
        /// Verifica se a atualização de parcelas já foi executada hoje.
        /// Caso não tenha sido, atualiza o status de 'A' para 'V' das parcelas vencidas
        /// e grava a data de execução na tabela de parâmetros do banco.
        /// </summary>
        /// <returns>True se executou a rotina hoje, False se já havia sido executada.</returns>
        public async Task<bool> VerificarEAtualizarAsync()
        {
            var hoje = DateTime.Today;

            // 1. Verificação ultra-rápida em memória (0 ms de overhead, sem bater no banco)
            if (_ultimaExecucaoEmMemoria.HasValue && _ultimaExecucaoEmMemoria.Value.Date == hoje)
            {
                return false;
            }

            await _semaphore.WaitAsync();
            try
            {
                // Re-checagem após obter o semáforo para evitar concorrência entre threads
                if (_ultimaExecucaoEmMemoria.HasValue && _ultimaExecucaoEmMemoria.Value.Date == hoje)
                {
                    return false;
                }

                using var ctx = await _dbFactory.CreateDbContextAsync();

                // 2. Garante que a tabela de parâmetros do sistema existe
                await ctx.Database.ExecuteSqlRawAsync(@"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ParametrosSistema')
                    BEGIN
                        CREATE TABLE ParametrosSistema (
                            Chave VARCHAR(50) NOT NULL PRIMARY KEY,
                            Valor VARCHAR(255) NULL,
                            DataAtualizacao DATETIME NULL
                        );
                    END
                ");

                // 3. Consulta no banco o parâmetro de última execução
                var hojeStr = hoje.ToString("yyyy-MM-dd");
                string valorParametro = null;

                var conn = ctx.Database.GetDbConnection();
                var deveFecharConexao = false;
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                    deveFecharConexao = true;
                }

                try
                {
                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT Valor FROM ParametrosSistema WHERE Chave = @chave";
                    var param = cmd.CreateParameter();
                    param.ParameterName = "@chave";
                    param.Value = ChaveParametro;
                    cmd.Parameters.Add(param);

                    var res = await cmd.ExecuteScalarAsync();
                    if (res != null && res != DBNull.Value)
                    {
                        valorParametro = res.ToString();
                    }
                }
                finally
                {
                    if (deveFecharConexao && conn.State == ConnectionState.Open)
                    {
                        await conn.CloseAsync();
                    }
                }

                // Se já constar no banco que foi executado na data de hoje, apenas memoriza
                if (valorParametro == hojeStr)
                {
                    _ultimaExecucaoEmMemoria = hoje;
                    return false;
                }

                // 4. Executa a atualização das parcelas vencidas no SQL Server
                await ctx.Database.ExecuteSqlRawAsync(@"
                    UPDATE Parcelas
                    SET Status = 'V'
                    WHERE Status = 'A'
                      AND CAST(DataVencimento AS DATE) < CAST(GETDATE() AS DATE);
                ");

                // 5. Salva a data de hoje no parâmetro no banco de dados
                await ctx.Database.ExecuteSqlRawAsync(@"
                    IF EXISTS (SELECT 1 FROM ParametrosSistema WHERE Chave = {0})
                        UPDATE ParametrosSistema 
                        SET Valor = {1}, DataAtualizacao = GETDATE() 
                        WHERE Chave = {0}
                    ELSE
                        INSERT INTO ParametrosSistema (Chave, Valor, DataAtualizacao) 
                        VALUES ({0}, {1}, GETDATE())
                ", ChaveParametro, hojeStr);

                _ultimaExecucaoEmMemoria = hoje;
                return true;
            }
            catch (Exception)
            {
                // Em caso de falha transitória de rede, não bloqueia o carregamento da tela
                return false;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}

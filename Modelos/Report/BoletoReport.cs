using System;

namespace SyncBoletos_WS.Models.Report
{
    public class BoletoReport
    {
        public int Id { get; set; }
        public string NumeroDocumento { get; set; }
        public string NomePagador { get; set; }
        public string CpfCnpj { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime Pagamento { get; set; }
        public decimal Valor { get; set; }
        public decimal Oscilacao { get; set; }
        public decimal ValorPago { get; set; }
        public string Status { get; set; }
        public string Doc { get; set; }
        public string Carteira { get; set; }
        public decimal ValorSped { get; set; }
        public decimal ValorTef { get; set; }
        public decimal ValorBackup { get; set; }
        public decimal ValorManifesto { get; set; }
        public decimal ValorComissao { get; set; }

    }
}
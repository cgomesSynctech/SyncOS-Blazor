using System;

namespace SyncBoletos_WS.Models.Report
{
    public class SacadoReport
    {
        public string Cliente { get; set; }
        public string CpfCnpj { get; set; }
        public string Bloqueado { get; set; }
        public decimal ValorContrato { get; set; }
        public decimal ValorBoleto { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime Pagamento { get; set; }
        public string StatusTitulo { get; set; }
    }
}
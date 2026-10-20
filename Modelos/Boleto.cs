using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Boleto
    {
        public int Id { get; set; }
        public string LinhaDigitavel { get; set; }
        public string NumeroDocumento { get; set; }
        public string CodigoBarras { get; set; }
        public string NossoNumero { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime Baixa { get; set; }
        public DateTime Pagamento { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Valor { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal PercentualMulta { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal MoraDia { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ValorPago { get; set; }
        public string InstrucoesLinha1 { get; set; }
        public string InstrucoesLinha2 { get; set; }
        public string Doc { get; set; }
        public byte[] Base64Pdf { get; set; }
        [MaxLength(1)]
        public string Cancelado { get; set; }

        public int  SacadoId { get; set; }
       
        public int StatusId { get; set; }

        public int CarteiraId { get; set; }
        public int UsuarioId { get; set; }
        public int RemessaId { get; set; }
    }
}
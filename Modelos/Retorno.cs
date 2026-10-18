using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos
{
    public class Retorno
    {
        public int Id { get; set; }
        public int CodigoOcorrencia { get; set; }
        public int BoletoId { get; set; }
        public int SacadoId { get; set; }
        public string NumeroDocumento { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ValorPago { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public DateTime Baixa { get; set; }


    }
}

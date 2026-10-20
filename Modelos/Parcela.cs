using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos
{
    public class Parcela
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name = "Nome do Cliente")]
        public string NomeCliente { get; set; }

        [MaxLength(14, ErrorMessage = "Máximo de 14 caracteres")]
        [Display(Name = "CPF/CNPJ")]
        public string CPFCNPJ { get; set; }

        [MaxLength(20, ErrorMessage = "Máximo de 20 caracteres")]
        [Display(Name = "Telefone")]
        public string Fone { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Vencimento")]
        public DateTime DataVencimento { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório")]
        [Column(TypeName = "decimal(10, 2)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Valor Pago")]
        public decimal? ValorPago { get; set; } = 0;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Pagamento")]
        public DateTime? DataPagamento { get; set; }

        [MaxLength(1)]
        [Display(Name = "Status")]
        public string Status { get; set; } = "A"; // A - A Vencer, V - Vencido, L - Liquidado 

        [MaxLength(500)]
        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [Display(Name = "Juros")]
        public decimal? Juros { get; set; }
    }
}

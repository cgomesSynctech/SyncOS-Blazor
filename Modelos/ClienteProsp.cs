using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Modelos
{
    public class ClienteProsp
    {
        [Key]
        public int Id { get; set; }
        public int RevendaId { get; set; }
        public int StatusId { get; set; }
        public int SetorId { get; set; }

        [Column(TypeName = "varchar(255)")]
        [Required(ErrorMessage = "Campo Obrigatório. Favor informar a alegação.")]
        [MinLength(5, ErrorMessage = "O campo deve ter no mínimo 5 caracteres.")]
        public string Razao { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required(ErrorMessage = "Campo Obrigatório. Favor informar a alegação.")]
        [MinLength(5, ErrorMessage = "O campo deve ter no mínimo 5 caracteres.")]
        public string Fantasia { get; set; }

        [Column(TypeName = "varchar(14)")]
        [Required(ErrorMessage = "Campo Obrigatório. Favor informar a alegação.")]
        [MinLength(11, ErrorMessage = "O campo deve ter no mínimo 11 caracteres.")]
        public string CPF_CNPJ { get; set; }

        [Column(TypeName = "varchar(255)")]
        [Required(ErrorMessage = "Campo Obrigatório. Favor informar a alegação.")]
        [MinLength(5, ErrorMessage = "O campo deve ter no mínimo 5 caracteres.")]
        public string Endereco { get; set; }

        [Column(TypeName = "varchar(255)")]
        [Required(ErrorMessage = "Campo Obrigatório. Favor informar a alegação.")]
        [MinLength(5, ErrorMessage = "O campo deve ter no mínimo 5 caracteres.")]
        public string Bairro { get; set; }

        [Column(TypeName = "varchar(10)")]
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "CEP inválido. Use o formato 00000-000 ou 00000000.")]
        public string CEP { get; set; }

        [Column(TypeName = "Char(2)")]
        public string UFId { get; set; }

        [Column(TypeName = "varchar(10)")]
        [Required(ErrorMessage = "Campo Obrigatório. Favor informar a alegação.")]
        [MinLength(1, ErrorMessage = "O campo deve ter no mínimo 5 caracteres.")]
        public string Numero { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double? ValorNegociacao { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double? ValorManutencaoNegociacao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataInclusao { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataAlteracao { get; set; } = DateTime.MinValue;

        public int MunicipioId { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string InscricaoEstadual { get; set; }


        [Column(TypeName = "varchar(255)")]
        [Required(ErrorMessage = "Campo Obrigatório. Favor informar a alegação.")]
        [MinLength(5, ErrorMessage = "O campo deve ter no mínimo 5 caracteres.")]
        public string Contato { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string WhatsApp { get; set; }

        [Column(TypeName = "varchar(255)")]
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Vendedor { get; set; }
        public bool Retaguarda { get; set; }
        public bool Nfc { get; set; }
        public bool Nf { get; set; }
        public bool MDF { get; set; }
        public bool VipShop { get; set; }
        public bool Sped { get; set; }
        public bool Backup { get; set; }
        public bool TEF { get; set; }
        public bool Outros { get; set; }
        public string EspecieOutros { get; set; }
        public bool Certificado { get; set; }
        public string PlanoPgto { get; set; }
        public string FormaPgto { get; set; } 
        public int Flag { get; set; } = 0;
        public virtual UF UF { get; set; }
        public virtual Municipio Municipio { get; set; }
        public virtual Revenda Revenda { get; set; }
        public virtual ClienteProspStatus Status { get; set; }
        public virtual Setor Setor { get; set; }
        public ICollection<ClienteProspHistorico> Historicos { get; set; } = new List<ClienteProspHistorico>();
    }
}

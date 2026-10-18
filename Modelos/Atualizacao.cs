using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public partial class Atualizacao
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataVersao { get; set; }
        [Required]
        public string Versao { get; set; }
        public string CNPJ { get; set; }
        public string LeiaMe { get; set; }
        public string Script { get; set; }
        public string Pacote { get; set; }
        public string PathDescompactar { get; set; }
        [Required]
        public int TipoAtualizacaoId { get; set; }

        [MaxLength(1)]
        [Required]
        public string Backup { get; set; }
        [MaxLength(1)]
        [Required]
        public string Ativo { get; set; }

        public virtual TipoAtualizacao TiposAPP { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Modelos
{
    public class ClienteProspHistorico
    {
        [Key]
        public int Id { get; set; }
        public int ClienteProspId { get; set; }
        public int UsuarioId { get; set; }

        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }
        public string Historico { get; set; }
        public string Arquivo { get; set; }

        public ClienteProsp ClienteProsp { get; set; } // Propriedade de navegação
        public virtual Usuario Usuario { get; set; }
    }
}

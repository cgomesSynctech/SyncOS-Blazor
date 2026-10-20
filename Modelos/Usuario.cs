using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string LoginName { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string LoginPass { get; set; }
        [Required]
        public int PerfilId { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Email { get; set; }
        public bool Ativo { get; set; } = true;

        [Column(TypeName = "varchar(32)")]
        public string? Role { get; set; }

        [NotMapped]
        [Column(TypeName = "varchar(255)")]
        public string? Token { get; set; }

        [NotMapped]
        [Column(TypeName = "varchar(50)")]
        public string? ConfirmePass { get; set; }

    }
}

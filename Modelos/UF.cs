using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Modelos
{
    public class UF
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Column(TypeName = "Char(2)")]
        public string Id { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Descricao { get; set; }
        public int Codigo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Revenda
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Descricao { get; set; }
        [Column(TypeName = "varchar(14)")]
        public string CNPJ { get; set; }

    }
}

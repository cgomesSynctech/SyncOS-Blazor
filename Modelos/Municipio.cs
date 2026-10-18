
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Modelos
{
    public class Municipio
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        public string UFId { get; set; }
        
        [Column(TypeName = "varchar(255)")]
        public string Descricao { get; set; }
        public int Ordem { get; set; } = 9990;

        public string Desc_UF { get { return string.Format("{0}-{1}", UFId, Descricao); }  }

        public virtual UF UF { get; set; }

    }
}

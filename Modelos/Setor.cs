using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Setor
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}

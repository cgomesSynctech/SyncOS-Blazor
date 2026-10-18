using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class GeradorID
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        [Required]
        public int UltimoID { get; set; }

    }
}

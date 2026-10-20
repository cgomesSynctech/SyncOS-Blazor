using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos
{
    public class StatusTitulo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}
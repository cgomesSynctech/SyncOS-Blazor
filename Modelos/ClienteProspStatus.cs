using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos
{
    public class ClienteProspStatus
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Descricao { get; set; }
    }
}

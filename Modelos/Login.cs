using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos
{
    [NotMapped]
    public class Login
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(32)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(32)")]
        public string PassWord { get; set; }
        [Column(TypeName = "varchar(32)")]
        public string ConfirmPassWord { get; set; }
    }
}

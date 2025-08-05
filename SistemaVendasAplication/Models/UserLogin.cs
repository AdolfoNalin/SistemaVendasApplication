using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaVendasAplication.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaVendasAplication.Models
{
    public class UserLogin
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
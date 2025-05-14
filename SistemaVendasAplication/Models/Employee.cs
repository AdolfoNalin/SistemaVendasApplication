using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace SistemaVendasAplication.Models
{
    public class Employee : Client
    {
        [Required]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Minimo de 2 caracteris e maximo de 10")]
        public string Login{ get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A Lista de aturização é obrigatório")]
        public List<string> Authorizations { get; set; }
    }
}

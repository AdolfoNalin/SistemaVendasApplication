using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SistemaVendasAplication.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Funcionário é Obrigatório")]
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Nome é Obrigatório!")]
        [MinLength(4, ErrorMessage = "Número minimo de caracteris é de 4", ErrorMessageResourceName = "Minimo")]
        [MaxLength(50, ErrorMessage = "Número maximo de caracteris é de 50", ErrorMessageResourceName = "Maximo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Login é Obrigatório!")]
        [MinLength(4, ErrorMessage = "Número minimo de caracteris é de 4", ErrorMessageResourceName = "Minimo")]
        [MaxLength(50, ErrorMessage = "Número maximo de caracteris é de 50", ErrorMessageResourceName = "Maximo")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha é obrgatória!")]
        [MaxLength(50, ErrorMessage = "Número maximo de caracteris é de 8", ErrorMessageResourceName = "Maximo")]
        public string Password { get; set; }

        [Required(ErrorMessage = "É necessário ter autorização")]
        [MaxLength(300, ErrorMessage = "Máximo de caracteris é de 300")]
        public List<String> Authorizations { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }

        [JsonIgnore]
        public Employee? Employee { get; set; }
    }
}
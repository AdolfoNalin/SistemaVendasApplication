using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendasAplication.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é Obrigatório!")]
        [MaxLength(50, ErrorMessage = "Número maximo de caracteris é de 50", ErrorMessageResourceName = "Maximo")]
        [MinLength(4, ErrorMessage = "Número minimo de caracteris é de 4", ErrorMessageResourceName = "Minimo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Login é Obrigatório!")]
        [MaxLength(50, ErrorMessage = "Número maximo de caracteris é de 50", ErrorMessageResourceName = "Maximo")]
        [MinLength(4, ErrorMessage = "Número minimo de caracteris é de 4", ErrorMessageResourceName = "Minimo")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha é obrgatória!")]
        [MaxLength(50, ErrorMessage = "Número maximo de caracteris é de 50", ErrorMessageResourceName = "Maximo")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Função do colaborador é obrigatória!")]
        public string Function { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace SistemaVendasAplication.Models
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "Minimo de 2 caracteris e o maximo de 200 caracteris")]
        public string Name { get; set; }
        
       [MaxLength(50,ErrorMessage = "Maximo de 50 caracteris")]
        public string? ShortName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate{ get; set; }

        [StringLength(12, MinimumLength = 3, ErrorMessage = "Verifique se todos os numeros estão corretos")]
        public string? RG { get; set; }

        [Required(ErrorMessage = "O Campo CPF é obrigatório!")]
        [StringLength(14,MinimumLength = 3, ErrorMessage = "Verifique se todos os caracteris estão certos!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo Estado Civil é obrigatório!")]
        [MaxLength(50, ErrorMessage = "O maximo de caracteris aceito é de 50")]
        [MinLength(5, ErrorMessage = "O minimo de Caracteris aceito é 5")]
        public string MaritalStatus { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public string? TelephoneNumber { get; set; }

        [Required(ErrorMessage = "Número de celular é obrigatório")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "Verifique se todos os números estão certos!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "CEP é obrigatótio!")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Digite o CEP corretamente")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatório")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O minino de caracteris é 2 e o maximo é 150")]
        public string City { get; set; }

        [Required(ErrorMessage = "Endereço é obrigatório")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O minino de caracteris é 2 e o maximo é 150")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Numero é obrigatório")]
        public int? Number { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatório")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O minino de caracteris é 2 e o maximo é 150")]
        public string Neighborhoods { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "O minino de caracteris é 2 e o maximo é 150")]
        public string State { get; set; }

        [MaxLength(300, ErrorMessage = "O Maximo de caracteris é 300")]
        public string? Complement { get; set; }

        [Required(ErrorMessage = "É necessário ter o valor do crediário")]
        public decimal Credit { get; set; }

        public decimal CreditLimit { get; set; }

        public Client()
        {
            Id = Guid.NewGuid();
        }
    }
}

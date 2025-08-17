using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace SistemaVendasAplication.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "Minimo de 2 caracteris e o maximo de 200 caracteris")]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimo de 2 caracteris e o máximo de 10 caracteris")]
        public string? ShortName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [StringLength(12, MinimumLength = 3, ErrorMessage = "Verifique se todos os numeros estão corretos")]
        [MaxLength(12)]
        public string? RG { get; set; }

        [Required(ErrorMessage = "O Campo CPF é obrigatório!")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Verifique se todos os numeros estão corretos")]
        public string CPF { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Estado civil é obrgatório!")]
        [MaxLength(50)]
        [MinLength(2)]
        public string MaritalStatus { get; set; }

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
        [StringLength(150, MinimumLength = 2, ErrorMessage = "O minino de caracteris é 2 e o maximo é 150")]
        public string Neighborhoods { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "O minino de caracteris é 2 e o maximo é 150")]
        public string State { get; set; }

        [MaxLength(300, ErrorMessage = "Numero maximo de caracteris é 300")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "A Lista de aturização é obrigatório")]
        public List<string> Authorizations { get; set; }

        [Required(ErrorMessage = "Função é obrigatória!")]
        [MaxLength(150, ErrorMessage = "O maximo de caracteris é 150")]
        [MinLength(2, ErrorMessage = "Minimo de caracteris é 2")]
        public string Function { get; set; }

        public Employee()
        {
            Id = Guid.NewGuid();
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SistemaVendasAplication.Models
{
    public class Supplier 
    {

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "Minimo de 2 caracteris e o maximo de 200 caracteris")]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimo de 2 caracteris e o máximo de 10 caracteris")]
        public string? ShotName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [MaxLength(200, ErrorMessage = "O Maximo de caracteris é 200")]
        [MinLength(2, ErrorMessage = "O minimo de caracteris é 2")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "O Campo CPF é obrigatório!")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "O minimo e o maximo é de 12 caracteris")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "CNPJ é obrigatório")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "Confira se todos os caracteris estão certo")]
        public string CNPJ { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [StringLength(9, MinimumLength = 9, ErrorMessage = "Verifique se todos os números estão certos!")]
        [DataType(DataType.PhoneNumber)]
        public string TelephoneNumber { get; set; }

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

        public int? Number { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatório")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O minino de caracteris é 2 e o maximo é 150")]
        public string Neighborhoods { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O minino de caracteris é 2 e o maximo é 150")]
        public string State { get; set; }

        public Supplier()
        {
            Id = Guid.NewGuid();
        }
    }
}

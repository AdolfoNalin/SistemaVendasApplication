using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SistemaVendasAplication.Models
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "Minimo de 2 caracteris e o maximo de 200 caracteris")]
        public string Name { get; set; }

        [StringLength(50,MinimumLength = 2, ErrorMessage = "Minimo de 2 caracteris e o máximo de 10 caracteris")]
        public string? ShotName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate{ get; set; }

        [Required(ErrorMessage = "O Campo RG é obrigatório!")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "O minimo e o maximo é de 15 caracteris")]
        public string RG { get; set; }

        [Required(ErrorMessage = "O Campo CPF é obrigatório!")]
        [StringLength(12,MinimumLength = 12, ErrorMessage = "O minimo e o maximo é de 12 caracteris")]
        public string CPF { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(9, MinimumLength = 9, ErrorMessage = "Verifique se todos os números estão certos!")]
        [DataType(DataType.PhoneNumber)]
        public string TelephoneNumber { get; set; }

        [Required(ErrorMessage = "Número de celular é obrigatório")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Verifique se todos os números estão certos!")]
        [DataType(DataType.PhoneNumber)]
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

        [StringLength(12, MinimumLength = 0, ErrorMessage = "O minino de caracteris é 0 e o maximo 0")]
        public int? Number { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatório")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O minino de caracteris é 2 e o maximo é 150")]
        public string Neighborhoods { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O minino de caracteris é 2 e o maximo é 150")]
        public string State { get; set; }

        public Client()
        {
            Id = Guid.NewGuid();
        }
    }
}

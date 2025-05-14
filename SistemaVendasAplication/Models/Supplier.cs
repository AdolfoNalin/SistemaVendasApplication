using System.ComponentModel.DataAnnotations;

namespace SistemaVendasAplication.Models
{
    public class Supplier : Client
    {
        [Required(ErrorMessage = "CNPJ é obrigatório")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "Confira se todos os caracteris estão certo")]
        public string CNPJ { get; set; }
    }
}

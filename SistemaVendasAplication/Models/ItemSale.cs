using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace SistemaVendasAplication.Models
{
    public class ItemSale
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Venda é obrigatória")]
        private Guid IdSale { get; set; }

        [Required(ErrorMessage = "Produto é obrigatório")]
        public Guid IdProduct { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatório")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Subtotal é obrigatório")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }

        public ItemSale()
        {
            Id = Guid.NewGuid();    
        }

        [JsonIgnore]
        public Sale? Sale { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}

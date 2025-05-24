using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SistemaVendasAplication.Models
{
    public class ItemBudget
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "É necessário ter o Orçamento")]
        public Guid IdBudget { get; set; }

        [Required(ErrorMessage = "Produto é obrigatório")]
        public Guid IdProduct { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatório")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Subtotal é obrigatório")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }

        public ItemBudget()
        {
            Id = Guid.NewGuid();
        }

        [JsonIgnore]
        public Budget? Budget { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}

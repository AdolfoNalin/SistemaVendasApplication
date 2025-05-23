using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaVendasAplication.Models
{
    public class ItemBudget : ItemSale
    {
        [Required(ErrorMessage = "É necessário ter o Orçamento")]
        public Guid IdBudget { get; set; }

        [Required(ErrorMessage = "É necessário ter o Produto")]
        public Guid IdProduct { get; set; }

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

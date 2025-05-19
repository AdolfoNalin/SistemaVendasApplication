using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaVendasAplication.Models
{
    public class ItemBudget : ItemSale
    {
        [Required]
        public Guid IdBudget { get; set; }

        [Required]
        public Guid Product { get; set; }

        public ItemBudget()
        {
            Id = Guid.NewGuid();
        }
    }
}

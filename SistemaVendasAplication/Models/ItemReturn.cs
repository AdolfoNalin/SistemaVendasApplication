using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace SistemaVendasAplication.Models
{
    public class ItemReturn
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Venda é obrigatória")]
        public Guid ReturnId { get; set; }    

        [Required(ErrorMessage = "Produto é obrigatório")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatório")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Subtotal é obrigatório")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }

        public ItemReturn()
        {
            Id = Guid.NewGuid();    
        }

        [JsonIgnore]
        public Sale? Sale { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}

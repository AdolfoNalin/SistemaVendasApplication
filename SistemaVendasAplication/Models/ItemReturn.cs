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

        [Required(ErrorMessage = "Id venda é obrigatório")]
        public Guid IdSale { get; set; }

        [Required(ErrorMessage = "Cliente é obrigatório")]
        public Guid IdClient { get; set; }

        [Required(ErrorMessage = "Produto é obrigatório")]
        public Guid IdProduct { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatório")]
        [Column(TypeName = "decimal(10,2)   ")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Motivo é obrigatório")]
        [StringLength(maximumLength: 200, MinimumLength = 10, ErrorMessage = "O valor Minimo de caracteris é 10 e o valor Maximo é 200. Por favor, cumbra o que é pedido!")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Valor total é obrigatório!")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [StringLength(maximumLength: 200, MinimumLength = 10, ErrorMessage = "O valor Minimo de caracteris é 10 e o valor Maximo é 200. Por favor, cumbra o que é pedido")]
        public string Obs { get; set; }

        public ItemReturn()
        {
            Id = Guid.NewGuid();
        }

        [JsonIgnore]
        public Sale? Sale { get; set; }

        [JsonIgnore]
        public Client? Client { get; set; }

        [JsonIgnore]
        public Product? Product{ get; set; }
    }
}

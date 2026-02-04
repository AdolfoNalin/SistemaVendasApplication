using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace SistemaVendasAplication.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Fornecedor é obrigatário")]
        public Guid SupplierId { get; set; }

        [Required(ErrorMessage = "Descrição completa é obrigatória")]
        [StringLength(maximumLength: 200, MinimumLength = 5, ErrorMessage = "")]
        public string FullDescription { get; set; }

        [Required(ErrorMessage = "Descrição resumida é obrigatória")]
        [StringLength(maximumLength: 200, MinimumLength = 5, ErrorMessage = "")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Preço avista é obrigatória")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal CashPrice { get; set; }

        [Required(ErrorMessage = "Preço a prazo é obrigatória")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TermPrice { get; set; }

        [Required(ErrorMessage = "Preço entrada é obrigatória")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal EntryPrice { get; set; }

        [Required(ErrorMessage = "Preço total é obrigatória")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Unidade de mediada é obrigatória")]
        [Column(TypeName = "varchar(50)")]
        public string UniMeasure { get; set; }

        [Required(ErrorMessage = "Data é obrigatória")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public Product()
        {
            Id = Guid.NewGuid();
        }

        [JsonIgnore]
        public Supplier? Supplier { get; set; }
    }
}

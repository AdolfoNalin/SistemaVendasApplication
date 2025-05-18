using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace SistemaVendasAplication.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Fornecedor é obrigatário")]
        public Guid IdSupllier { get; set; }

        [Required(ErrorMessage = "Descrição completa é obrigatória")]
        [StringLength(maximumLength: 200, MinimumLength = 10, ErrorMessage = "")]
        public string FullDescription { get; set; }

        [Required(ErrorMessage = "Descrição resumida é obrigatória")]
        [StringLength(maximumLength: 200, MinimumLength = 10, ErrorMessage = "")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Preço avista é obrigatória")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal CashPrice{ get; set; }

        [Required(ErrorMessage = "Preço a prazo é obrigatória")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TermPrice { get; set; }

        [Required(ErrorMessage = "Preço entrada é obrigatória")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal entryPrice { get; set; }

        [Required(ErrorMessage = "Preço total é obrigatória")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória")]
        [Column(TypeName = "decimal(10,2)")]
        public double Amount{ get; set; }

        [Required(ErrorMessage = "Unidade de Medida é obrigatória")]
        public string UnitMeasure{ get; set; }

        [Required(ErrorMessage = "SubTotal é obrigatória")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }

        [Required(ErrorMessage = "Data é obrigatória")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Time é obrigatória")]
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }

        public Product()
        {
            Id = Guid.NewGuid();
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SistemaVendasAplication.Models
{
    public class Budget
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "É Nessario ter um vendedor válido para continuar o orçamento")]
        public Guid IdEmployee { get; set; }

        [Required(ErrorMessage = "É necessário ter um cliente para a realização do orçamento")]
        public Guid IdClient { get; set; }

        [Required(ErrorMessage = "È necessario ter um produto para realizar o orçamento")]
        public Guid IdProduct { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal DescountPorcentage { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CashDescount { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal AdditionPorcentage { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal AdditionCash { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Changes { get; set; }

        [Required(ErrorMessage = "É necessário de data válida")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "É necessário hora válida")]
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }

        public Budget()
        {
            Id = Guid.NewGuid();
        }

        [JsonIgnore]
        public Employee? Employee { get; set; }

        [JsonIgnore]
        public Client? Client { get; set; }
    }
}

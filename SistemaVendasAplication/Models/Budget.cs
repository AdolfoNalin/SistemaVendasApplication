using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace SistemaVendasAplication.Models
{
    public class Budget
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "É Nessario ter um vendedor válido para continuar o orçamento")]
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "É necessário ter um cliente para a realização do orçamento")]
        public Guid ClientId { get; set; }

        [Required(ErrorMessage = "É necessário a forma de pagamento")]
        [MaxLength(50, ErrorMessage = "O máximo de caracteris é de 50")] 
        public string PaymentMethod { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal DescountPercentage { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CashDescount { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal AdditionPercentage { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal AdditionCash { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "É necessário de data válida")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [MaxLength(500,ErrorMessage = "Maximo de Caracteris é 500")]
        public string? Obs { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória")]
        public int AmountItens { get; set; }

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

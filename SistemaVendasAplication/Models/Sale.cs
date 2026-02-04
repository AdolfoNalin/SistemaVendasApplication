using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaVendasAplication.Models
{
    public enum OpenOrClose
    {
        Open,
        Close
    }
    public class Sale 
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "É necessário o caixa")]
        public Guid CashId { get; set; }

        [Required(ErrorMessage = "É Nessario ter um vendedor válido para continuar o orçamento")]
        public Guid EmployeeId { get; set; }
 
        [Required(ErrorMessage = "É necessário ter um cliente para a realização do orçamento")]
        public Guid ClientId { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage = "O numero maximo de caracteris é de 50")]
        [MinLength(2,ErrorMessage = "O numero minimo de caracteris é de 2")]
        public string PaymentMethod { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PercentageDiscount { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CashDiscount { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal AdditionPorcentage { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal AdditionCash { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [MaxLength(300, ErrorMessage = "Número maximo de caracteris é de 300")]
        public string Observation { get; set; }

        [Required(ErrorMessage = "É necessário a data!")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "É necessário a data de Vencimento")]            
        public DateTime ExpirationDate { get; set; }

        public int MyProperty { get; set; }

        [Required(ErrorMessage = "Aber ou fechado")]
        public OpenOrClose Open { get; set; }

        public Sale()
        {
            Id = Guid.NewGuid();
        }

        [JsonIgnore]
        public Employee? Employee { get; set; }

        [JsonIgnore]
        public Client? Client { get; set; }
    }
}

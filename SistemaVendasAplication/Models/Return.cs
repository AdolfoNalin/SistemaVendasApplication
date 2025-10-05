using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SistemaVendasAplication.Models
{
    public class Return
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "É Nessario ter um vendedor válido para continuar o venda")]
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "É necessário ter um cliente para a realizar do venda")]
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

        public Return()
        {
            Id = Guid.NewGuid();
        }

        [JsonIgnore]
        public Employee? Employee { get; set; }

        [JsonIgnore]
        public Client? Client { get; set; }
    }
}
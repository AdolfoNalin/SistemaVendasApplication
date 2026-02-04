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

        [Required(ErrorMessage = "É necessário uma venda para realizar a devlução")]
        public Guid SaleId { get; set; }

        [Required(ErrorMessage = "É Nessario ter um vendedor válido para continuar o Devolução")]
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "É necessário ter um cliente para a realizar a Venda")]
        public Guid ClientId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

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

        [JsonIgnore]
        public Sale? Sale { get; set; }
    }
}
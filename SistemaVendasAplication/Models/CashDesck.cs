using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SistemaVendasAplication.Models
{
    public class CashDesck
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "É necessário um usuário para abriar o caixa")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "É necessário data")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "É necessário o total")]
        public decimal Total { get; set; }

        public CashDesck()
        {
            Id = Guid.NewGuid();
        }

        [JsonIgnore]
        public User? User { get; set; }
    }
}
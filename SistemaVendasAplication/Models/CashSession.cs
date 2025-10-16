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
    public enum IsCashDesck
    {
        close = 1,
        Open
    }
    public class CashDesck
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "É necessário um usuário para abriar o caixa")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "É necessário data")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "É necessário a informar se é Entrada ou Saida")]
        public decimal OpeningAmount  { get; set; }

        [Required(ErrorMessage = "É necessário o Status")]
        public IsCashDesck Status { get; set; }

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
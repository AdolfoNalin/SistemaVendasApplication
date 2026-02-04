using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SistemaVendasAplication.Models
{
    public enum Type
    {
        entry,
        exit
    }

    public class CashMoviment
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "É necessário ter um caixa aberto")]
        public Guid CashSessionId { get; set; }

        [Required(ErrorMessage = "Usuário é necessário")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "É necessário o valor da movimentação")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "É necessário o tipo de movimentação")]
        public Type Type { get; set; }

        [Required(ErrorMessage = "É necessário ter a Descrição")]
        [MaxLength(300, ErrorMessage = "Limite de até 300 caracteris")]
        public string Description { get; set; }

        [Required(ErrorMessage = "É necessário uma a data de movimentação")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public CashMoviment()
        {
            Id = Guid.NewGuid();
        }

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public CashSession? CashSession { get; set; } 
    }
}
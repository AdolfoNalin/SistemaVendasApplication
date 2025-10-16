using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendasAplication.Models
{
    public class CashMoviment
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "É necessário ter um caixa aberto")]
        public Guid CashSession { get; set; }

        [Required(ErrorMessage = "É necessário o valor da movimentação")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "É necessário ter a Descrição")]
        [MaxLength(300, ErrorMessage = "Limite de até 300 caracteris")]
        public string Description { get; set; }

        [Required(ErrorMessage = "É necessário uma a data de movimentação")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}
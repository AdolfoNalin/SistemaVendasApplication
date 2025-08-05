using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVendasAplication.Data;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class ItemBudgetController : Controller
    {
        private SysComAppDBContext _context;

        public ItemBudgetController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region GetIdItens
        [HttpGet("product")]
        public async Task<IActionResult> Get([FromQuery] Guid idBudget, Guid? idProduct)
        {
            try
            {
                List<ItemBudget> budgets = await _context.ItemBudget.Where(i => i.BudgetId.ToString().Contains(idBudget.ToString())).ToListAsync()
                ?? throw new ArgumentNullException("Orçamento não existe");

                if (idProduct.ToString() == String.Empty)
                {
                    return Ok(budgets);
                }
                else
                {
                    List<ItemBudget> itens = budgets.Where(i => i.ProductId.ToString().Contains(idProduct.ToString())).ToList()
                    ?? throw new ArgumentNullException("Arguemnto NULO", "Produto não existe");

                    return Ok(itens);
                }
            }
            catch (ArgumentNullException ane) when (idBudget.ToString() == String.Empty || idProduct.ToString() == String.Empty)
            {
                return NotFound(ane.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region PostItens
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItemBudget item)
        {
            try
            {
                if (item is null)
                {
                    throw new ArgumentNullException("Os Itens são nulos");
                }
                else
                {
                    await _context.AddAsync(item);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest(false);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region PutItens
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ItemBudget item)
        {
            try
            {
                if (item is null)
                {
                    throw new ArgumentNullException("Item é nulo");
                }
                else
                {
                    _context.ItemBudget.Update(item);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok(true);
                    }
                    else {
                        return BadRequest(false);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion
    
    }
}
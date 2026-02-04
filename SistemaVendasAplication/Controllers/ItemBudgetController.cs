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
        [HttpGet("{idBudget}")]
        public async Task<IActionResult> Get([FromRoute] Guid idBudget)
        {
            try
            { 
                List<ItemBudget> itens = new List<ItemBudget>();
                if(idBudget == Guid.Empty)
                {
                    throw new ArgumentNullException("É necessário um Orçamento");
                }
                else
                {
                    itens = await _context.ItemBudget.Where(i => i.BudgetId == idBudget).ToListAsync()
                    ?? throw new ArgumentNullException("Nenhum item encontrado");
                    return Ok(itens);
                }

            }
            catch(ArgumentException ane)
            {
                return NotFound(ane.Message);
            }
            catch (System.Exception ex)
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
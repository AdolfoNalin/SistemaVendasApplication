using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVendasAplication.Data;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BudgetController : Controller
    {
        private SysComAppDBContext _context;

        public BudgetController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _context.Budget.OrderBy(b => b.Id).ToListAsync());
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region Getid
        [HttpGet]
        [Route("[id]")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _context.Budget.Where<Budget>(b => b.Id == id).ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetString
        [HttpGet]
        [Route("[value]")]
        public async Task<IActionResult> GetString(string value)
        {
            try
            {
                List<Budget> budgets = await _context.Budget.Where<Budget>(b => b.Client.Name.ToUpper().Contains(value) || b.Employee.Name.ToUpper().Contains(value)).ToListAsync()
                ?? throw new ArgumentNullException("Lista de orçamentos não encontrada!");

                return Ok(budgets);
            }
            catch (ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> Post(Budget budget)
        {
            try
            {
                if (budget is null)
                {
                    return NotFound("Arquivo não pode ser nulo");
                }

                await _context.Budget.AddAsync(budget);
                int value = await _context.SaveChangesAsync();

                if (value == 1)
                {
                    return Ok("Orçamento cadastrado com sucesso!");
                }
                else
                {
                    return BadRequest("O orçamento não foi salvo. Verifique os dados!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Put
        [HttpPut]
        public async Task<IActionResult> Put(Budget budget)
        {
            try
            {
                if (budget is null)
                {
                    return BadRequest("O orçamento está nulo");
                }

                if (_context.Budget.Any(b => b.Id == budget.Id))
                {
                    _context.Budget.Update(budget);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Orçamento atualizado com sucesso");
                    }
                    else
                    {
                        return NotFound("Orçamento não foi salvo. Verique os dados!");
                    }
                }
                else
                {
                    return NotFound("Orçamento não encontrado. Verifique se o orçamento está salvo!");
                }
            }
            catch (HttpRequestException hre)
            {
                return BadRequest(hre.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Delete
        [HttpDelete]
        public async Task<IActionResult> Delete(Budget budget)
        {
            try
            {
                if (budget is null)
                {
                    return BadRequest("Orçamento é nulo");
                }
                else
                {
                    if (_context.Budget.Any(b => b.Id == budget.Id))
                    {
                        _context.Budget.Remove(budget);
                        int value = await _context.SaveChangesAsync();

                        if (value == 1)
                        {
                            return Ok("Orçamento deletado com sucesso!");
                        }
                        else
                        {
                            return NotFound("Orçamento não deletado. Verifiqe se o ID está corredo");
                        }
                    }
                    else
                    {
                        return NotFound("Orçamento não encontrado. Verifique se o orçamento está salvo!");
                    }
                }
            }
            catch (HttpRequestException hre)
            {
                return BadRequest(hre.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
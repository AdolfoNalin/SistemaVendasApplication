using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.EntityFrameworkCore;
using SistemaVendasAplication.Data;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
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
                return NotFound(ex.Message);
            }
        }
        #endregion

        #region Getid
        [HttpGet]
        [Route("search/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                List<Budget> budgets = new List<Budget>();

                if (id.ToString() == String.Empty)
                {
                    throw new ArgumentNullException("Id é nulo");
                }
                else if (await _context.Budget.AnyAsync(i => i.Id.ToString().Contains(id.ToString())))
                {
                    budgets = await _context.Budget.Where(b => b.Id.ToString().Contains(id.ToString())).ToListAsync();
                }
                else
                {
                    return BadRequest("Orçamento não existe");
                }

                return Ok(await _context.Budget.Where<Budget>(b => b.Id == id).ToListAsync());
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

        #region GetString
        [HttpGet]
        [Route("smart/{value}")]    
        public async Task<IActionResult> GetString([FromRoute] string value)
        {
            try
            {
                List<Budget> budgets = await _context.Budget.OrderBy(b => b.Date).ToListAsync()
                ?? throw new ArgumentNullException("Lista de orçamentos não encontrada!");

                List<Client> clients = await _context.Client.OrderBy(c => c.Name).ToListAsync<Client>()
                ?? throw new ArgumentNullException("Clientes não existe");

                List<Employee> employees = await _context.Employee.OrderBy(e => e.Name).ToListAsync()
                ?? throw new ArgumentNullException("Funcionário não existe");

                budgets = budgets.Select(b =>
                {
                    b.Client =  clients.FirstOrDefault(c => c.Id.ToString().Contains(c.Id.ToString())) ?? throw new ArgumentNullException("Cliente não existe");
                    return b;
                }
                ).ToList();

                budgets = budgets.Select(b =>
                {
                    b.Employee = employees.FirstOrDefault(e => e.Id.ToString().Contains(b.EmployeeId.ToString()));
                    return b;
                }).ToList();

                budgets = budgets.Where(b => b.Client.Name.ToUpper().Contains(value.ToUpper())|| b.Employee.Name.ToUpper().Contains(value.ToUpper())).ToList();

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
        public async Task<IActionResult> Post([FromBody] Budget budget)
        {
            try
            {
                if (budget is null)
                {
                    throw new ArgumentNullException("Arquivo não pode ser nulo");
                }
                else if (await _context.Budget.AnyAsync(b => b.Id == budget.Id))
                {
                    throw new ArgumentException("Orçamento existente");
                }
                else if (budget != null && await _context.Budget.AnyAsync(b => b.Id == budget.Id) == false)
                {
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
                else
                {
                    throw new Exception("Aconteceu um erro!");
                }
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

        #region Put
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Budget budget)
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
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                if (id.ToString() is null || id.ToString() == String.Empty)
                {
                    return BadRequest("Orçamento é nulo");
                }
                else
                {
                    if (_context.Budget.Any(b => b.Id.ToString().Contains(id.ToString())))
                    {
                        Budget budget = await _context.Budget.FirstAsync(b => b.Id.ToString().Contains(id.ToString()));
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class SaleController : Controller
    {
        private SysComAppDBContext _context;

        public SaleController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Sale> sales = await _context.Sale.OrderBy(s => s.Date).ToListAsync()
                ?? throw new ArgumentNullException("Lista de vendas está vazia!");

                return Ok(sales);
            }
            catch (ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region GetOrderOpen
        [HttpGet("OrderOpen/{ClientId}")]
        public async Task<IActionResult> GetOrderOpen([FromRoute] Guid clientId)
        {
            try
            {
                List<Sale> sales = await _context.Sale.Where(s => s.ClientId == clientId
                && s.Open == OpenOrClose.Open).ToListAsync()
                ?? throw new ArgumentNullException("Nenhuma nota foi encontrada!");

                return Ok(sales);
            }
            catch(ArgumentNullException ane)
            {
                return NotFound(ane.ParamName);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region LastSale
        [HttpGet("Last")]
        public async Task<IActionResult> LastSale()
        {
            try
            {
                Sale sale = await _context.Sale.OrderBy(s => s.Date)
                .LastOrDefaultAsync()
                ?? throw new ArgumentNullException("Nenhum resultado encontrado!");

                return Ok(sale);
            }
            catch (ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region GetId
        [HttpGet("search/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                List<Sale> sales = await _context.Sale.Where(s => s.Id.ToString().Contains(id.ToString())).ToListAsync()
                ?? throw new ArgumentNullException("Lista de vendas está vazia");

                return Ok(sales);
            }
            catch (ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region GetCash
        [HttpGet("CashSession/{cashId}")]
        public async Task<IActionResult> GetCash([FromRoute] Guid cashId)
        {
            try
            {
                List<Sale> sales = await _context.Sale.Where(s => s.CashId == cashId).ToListAsync()
                ?? throw new ArgumentNullException("Nenhuma venda encontrada");

                return Ok(sales);
            }
            catch (ArgumentNullException ane)
            {
                return NotFound(ane.ParamName);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion 

        #region GetString
        [HttpGet("smart/{value}")]
        public async Task<IActionResult> Get([FromRoute] string value)
        {
            try
            {
                List<Sale> sales = await _context.Sale.OrderBy(s => s.Date).ToListAsync()
                ?? throw new ArgumentNullException("Não existe venda");

                List<Client> clients = await _context.Client.OrderBy(c => c.Name).ToListAsync()
                ?? throw new ArgumentNullException("Cliente não existe");

                List<Employee> employees = await _context.Employee.OrderBy(e => e.Name).ToListAsync()
                ?? throw new ArgumentNullException("Funcionário não existe");

                sales = sales.Select(s =>
                {
                    s.Client = clients.FirstOrDefault(c => c.Id.ToString().Contains(s.ClientId.ToString()));

                    s.Employee = employees.FirstOrDefault(e => e.Id.ToString().Contains(s.Id.ToString()));

                    return s;
                }).ToList();

                sales = sales.Where(s => s.Client.Name.ToUpper().Contains(value.ToUpper())
                || s.Employee.Name.ToUpper().Contains(value.ToUpper())).ToList();

                return Ok(sales);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region GetDate
        [HttpGet("Date")]
        public async Task<IActionResult> Get([FromQuery] DateTime startDate, DateTime endDate)
        {
            try
            {
                DateTime start = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
                DateTime end = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

                List<Sale> sales = await _context.Sale.Where(s => s.Date.Date >= start.Date
                && s.Date.Date <= end.Date).ToListAsync()
                ?? throw new ArgumentNullException("Nenhuma venda foi encontrada neste período");

                return Ok(sales);
            }
            catch (ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Sale sale)
        {
            try
            {
                if (sale is null)
                {
                    throw new ArgumentNullException("Venda é nula");
                }
                else if (await _context.Sale.AnyAsync(s => s.Id == sale.Id))
                {
                    throw new ArgumentException("Venda já existente no banco de dados");
                }
                else if (sale != null && await _context.Sale.AnyAsync(s => s.Id == sale.Id) == false)
                {
                    await _context.Sale.AddAsync(sale);
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
                else
                {
                    throw new Exception("Aconteceu um erro");
                }
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region Put
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Sale sale)
        {
            try
            {
                if (sale is null)
                {
                    throw new ArgumentNullException("Venda é nula");
                }
                else if (await _context.Sale.AnyAsync(s => s.Id == sale.Id) == false)
                {
                    throw new ArgumentException("Venda não existe no banco de dados");
                }
                else if (await _context.Sale.AnyAsync(s => s.Id == sale.Id))
                {
                    _context.Sale.Update(sale);
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
                else
                {
                    return BadRequest("Aconteceu um erro");
                }
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region Put
        [HttpPut("PayOff")]

        public async Task<IActionResult> PutPayOff([FromBody] Sale sale)
        {
            try
            {
                if(sale is null)
                {
                    throw new ArgumentNullException("Id guid está vázio");
                }
                else if(await _context.Sale.AnyAsync(s => s.Id == sale.Id))
                {
                    sale.Open = OpenOrClose.Close;

                    _context.Sale.Update(sale);
                    int value = await _context.SaveChangesAsync();

                    if(value == 1)
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Aconteceu um erro ao salvar");
                    }
                }
                else
                {
                    throw new ArgumentException("Venda não foi encontrada");
                }
            }
            catch(ArgumentNullException ane)
            {
                return NotFound(ane.ParamName);
            }
            catch(ArgumentException ae)
            {
                return NotFound(ae.Message);
                
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }

        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Detele([FromRoute] Guid id)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    throw new ArgumentNullException("Venda é nula");
                }
                else if (await _context.Sale.AnyAsync(s => s.Id == id) == false)
                {
                    throw new ArgumentException("Venda não existe no banco de dados");
                }
                else if (await _context.Sale.AnyAsync(s => s.Id == id) && id != null)
                {
                    Sale sale = await _context.Sale.Where(s => s.Id == id).FirstOrDefaultAsync();
                    _context.Sale.Remove(sale);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Venda foi excluida");
                    }
                    else
                    {
                        return BadRequest("Venda não foi deletada");
                    }
                }
                else
                {
                    throw new Exception("Aconteceu um erro");
                }
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(error: $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion
    }
}
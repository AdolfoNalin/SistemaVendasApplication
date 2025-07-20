using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SistemaVendasAplication.Data;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ItemReturnController : Controller
    {
        private SysComAppDBContext _context;

        public ItemReturnController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<ItemReturn> itens = await _context.ItemReturn.OrderBy(i => i.Date).ToListAsync()
                ?? throw new ArgumentNullException("Lista de itens está vazia");

                return Ok(itens);
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

        #region GetDate
        [HttpGet("Date")]
        public async Task<IActionResult> Get([FromQuery] DateTime startDate, DateTime endDate)
        {
            try
            {
                List<ItemReturn> itens = await _context.ItemReturn.Where(i => i.Date.ToString().Contains(startDate.ToString())
                && i.Date.ToString().Contains(endDate.ToString()))
                .OrderBy(i => i.Date).ToListAsync()
                ?? throw new ArgumentNullException("Lista de itens está vazia");

                return Ok(itens);
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

        #region GetSmart
        [HttpGet("Smart/{value}")]
        public async Task<IActionResult> Get([FromRoute] string value)
        {
            try
            {
                List<Client> clients = await _context.Client.ToListAsync() ??
                throw new ArgumentNullException("Lista de Cliente está vazia");

                List<ItemReturn> itens = await _context.ItemReturn.OrderBy(i => i.Date).ToListAsync()
                ?? throw new ArgumentNullException("Lista de Devolução está vazia");

                itens = itens.Select(i =>
                {
                    i.Client = clients.FirstOrDefault(c => c.Id.ToString().Contains(i.ClientId.ToString()));
                    return i;
                }).ToList();

                if (itens is null)
                {
                    throw new ArgumentNullException($"Devolção de venda não existe no nome desse cliente");
                }
                else
                {
                    itens = itens.Where(i => i.Client.Name.ToUpper().Contains(value.ToUpper())).ToList();
                    return Ok(itens);
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

        #region Post
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItemReturn itens)
        {
            try
            {
                if (itens is null)
                {
                    throw new ArgumentNullException("Devolução é nulo");
                }
                else if (await _context.ItemReturn.AnyAsync(i => i.Id.ToString().Contains(itens.Id.ToString())))
                {
                    throw new ArgumentException("Esse venda já foi realizada a devolução");
                }
                else if (await _context.ItemReturn.AnyAsync(i => i.Id.ToString().Contains(itens.Id.ToString())) == false
                || itens != null)
                {
                    itens.Date = itens.Date.ToUniversalTime();
                    await _context.ItemReturn.AddAsync(itens);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Devolução cadastrada com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Não foi possivel cadastrar a devolução");
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
        public async Task<IActionResult> Put([FromBody] ItemReturn itens)
        {
            try
            {
                if (itens is null)
                {
                    throw new ArgumentNullException("Devolução está nulo");
                }
                else if (await _context.ItemReturn.AnyAsync(i => i.Id.ToString().Contains(itens.Id.ToString())) == false)
                {
                    throw new ArgumentException("Devolução não pode ser atualizado. Porque não existen no banco de dados");
                }
                else if (await _context.ItemReturn.AnyAsync(i => i.Id.ToString().Contains(itens.Id.ToString()))
                || itens != null)
                {
                    _context.ItemReturn.Update(itens);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Devolução foi atualziada com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Devolução não foi ataulizada com sucesso!");
                    }
                }
                else
                {
                    throw new Exception("Aconteceu um erro");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
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
                    throw new ArgumentNullException("Item é nulo. Impossivél de deletar");
                }
                else if (await _context.ItemReturn.AnyAsync(i => i.Id.ToString().Contains(id.ToString())) == false)
                {
                    throw new ArgumentException("Devolução não existe no banco de dados");
                }
                else if (await _context.ItemReturn.AnyAsync(i => i.Id.ToString().Contains(id.ToString())))
                {
                    ItemReturn item = await _context.ItemReturn.FirstAsync(i => i.Id.ToString().Contains(id.ToString()));
                    _context.ItemReturn.Remove(item);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Devolução foi removida com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Não foi possivel realizar a devolução!");
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
    }
}
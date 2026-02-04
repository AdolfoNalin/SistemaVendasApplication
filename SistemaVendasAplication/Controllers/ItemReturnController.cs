using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SistemaVendasAplication.Data;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class ItemReturnController : Controller
    {
        private SysComAppDBContext _context;

        public ItemReturnController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet("{returnId}")]
        public async Task<IActionResult> Get([FromRoute] Guid returnId)
        {
            try
            {
                List<ItemReturn> itens = await _context.ItemReturn.Where(i => i.ReturnId == returnId).ToListAsync()
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

        #region GetIdReturn
        [HttpGet("returnId/{returnId}")]
        public async Task<IActionResult> GetIdReturn([FromRoute] Guid returnId)
        {
            try
            {
                ItemReturn @return = await _context.ItemReturn.Where(i => i.ReturnId == returnId).FirstOrDefaultAsync()
                ?? throw new ArgumentNullException("Nehum item com esse ID");

                return Ok(@return.Id);
            }
            catch (ArgumentNullException ane)
            {
                return NotFound(ane.ParamName);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region GetDate
        [HttpGet("Product")]
        public async Task<IActionResult> Get([FromQuery] Guid returnId, Guid productId)
        {
            try
            {
                List<ItemReturn> itens = await _context.ItemReturn.Where(i => i.ReturnId == returnId &&
                i.ProductId == productId).ToListAsync()
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
                Client client = await _context.Client.Where(c => c.Name.Contains(value.ToUpper())
                || c.ShortName.ToUpper().Contains(value.ToUpper())).FirstOrDefaultAsync()
                ?? throw new ArgumentNullException("Nenhum Cliente econtrado");

                List<Return> returns = await _context.Return.Where(i => i.ClientId == client.Id).ToListAsync() ??
                throw new ArgumentException("Cliente não realizou nenhum devolução");

                return Ok(returns);
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
        public async Task<IActionResult> Post([FromBody] BindingList<ItemReturn> itens)
        {
            try
            {
                if (itens is null)
                {
                    throw new ArgumentNullException("Devolução é nulo");
                }
                else if (await _context.ItemReturn.AnyAsync(i => i.Id.ToString().Contains(itens[0].Id.ToString())))
                {
                    throw new ArgumentException("Esse venda já foi realizada a devolução");
                }
                else if (await _context.ItemReturn.AnyAsync(i => i.Id.ToString().Contains(itens[0].Id.ToString())) == false
                || itens != null)
                {
                    int value = 0;
                    foreach (ItemReturn item in itens)
                    {
                        await _context.ItemReturn.AddAsync(item);
                        value = await _context.SaveChangesAsync();

                        if (value != 1)
                        {
                            break;
                        }
                    }
                    
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
        public async Task<IActionResult> Put([FromBody] BindingList<ItemReturn> itens)
        {
            try
            {
                if (itens is null)
                {
                    throw new ArgumentNullException("Devolução está nulo");
                }
                else if (await _context.ItemReturn.AnyAsync(i => i.Id == itens[0].Id) == false)
                {
                    throw new ArgumentException("Devolução não pode ser atualizado. Porque não existen no banco de dados");
                }
                else if (await _context.ItemReturn.AnyAsync(i => i.Id == itens[0].Id)
                || itens != null)
                {
                    List<ItemReturn> returns = _context.ItemReturn.Where(i => i.Id == itens[0].Id).ToList();

                    int value = 0;
                    foreach (ItemReturn item in returns)
                    {
                        value = 0;
                        _context.ItemReturn.Remove(item);
                        value = await _context.SaveChangesAsync();

                        if (value == 1)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    
                    int result = 0;
                    if (value == 1)
                    {
                        foreach (ItemReturn item in itens)
                        {
                            result = 0;
                            await _context.ItemReturn.AddAsync(item);
                            value = await _context.SaveChangesAsync();

                            if (value == 1)
                            {
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    if (result == 1)
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
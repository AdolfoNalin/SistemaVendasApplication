using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVendasAplication.Data;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ItemSaleController : Controller
    {
        private SysComAppDBContext _context;

        public ItemSaleController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid idSale)
        {
            try
            {
                List<ItemSale> itens = await _context.ItemSale.Where(i => i.SaleId.ToString().Contains(idSale.ToString())).ToListAsync()
                ?? throw new ArgumentNullException("Lista de itens está vazia");

                return Ok(itens);
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
        public async Task<IActionResult> Post([FromBody] ItemSale item)
        {
            try
            {
                if (item is null)
                {
                    throw new ArgumentNullException("Itens são nulos");
                }
                else if (await _context.ItemSale.AnyAsync(i => i.SaleId.ToString().Contains(item.SaleId.ToString())))
                {
                    throw new ArgumentException("Não é possivél salvar uma venda que já existe");
                }
                else if (await _context.ItemSale.AnyAsync(i => i.SaleId.ToString().Contains(item.SaleId.ToString())) == false
                && item is null)
                {
                    await _context.ItemSale.AddAsync(item);
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
                    return BadRequest("Aconteceu um erro!");
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
        public async Task<IActionResult> Put([FromBody] ItemSale item)
        {
            try
            {
                if (item is null)
                {
                    throw new ArgumentNullException("Item é nulo");
                }
                else if (await _context.ItemSale.AnyAsync(i => i.SaleId.ToString().Contains(item.SaleId.ToString())) == false)
                {
                    throw new ArgumentException("Venda não existe no banco de dados");
                }
                else if (await _context.ItemSale.AnyAsync(i => i.SaleId.ToString().Contains(item.SaleId.ToString()))
                && item is null)
                {
                    _context.ItemSale.Update(item);
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
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion
    }
}
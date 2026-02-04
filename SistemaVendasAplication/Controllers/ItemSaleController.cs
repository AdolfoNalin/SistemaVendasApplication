using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
    public class ItemSaleController : Controller
    {
        private SysComAppDBContext _context;

        public ItemSaleController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet("{idSale}")]
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

        #region GetItemSale 
        [HttpGet("Sale/{saleId}")]
        public async Task<IActionResult> GetItemSale([FromRoute] Guid saleId)
        {
            try
            {
                if (saleId == Guid.Empty)
                {
                    return NotFound("É necessário o Id da venda");
                }
                else if (_context.ItemSale.Any(i => i.SaleId.ToString().Contains(saleId.ToString())))
                {
                    ItemSale item = await _context.ItemSale.Where(i => i.SaleId.ToString().Contains(saleId.ToString())).FirstAsync();
                    return Ok(item.Id);
                }
                else
                {
                    return BadRequest("Id venda está incorreto!");
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<ItemSale> itens)
        {
            try
            {
                if (itens[0].SaleId == null)
                {
                    throw new ArgumentNullException("Itens são nulos");
                }
                else if (await _context.ItemSale.AnyAsync(i => i.SaleId == itens[0].SaleId))
                {
                    throw new ArgumentException("Não é possivél salvar uma venda que já existe");
                }
                else if (await _context.ItemSale.AnyAsync(i => i.SaleId == itens[0].SaleId) == false
                && itens[0].SaleId != null)
                {
                    int value = 0;
                    foreach (ItemSale item in itens)
                    {
                        value = 0;
                        await _context.ItemSale.AddAsync(item);
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
        public async Task<IActionResult> Put([FromBody] List<ItemSale> itens)
        {
            try
            {
                if (itens is null)
                {
                    throw new ArgumentNullException("Item é nulo");
                }
                else if (await _context.ItemSale.AnyAsync(i => i.SaleId == itens[0].SaleId) == false)
                {
                    throw new ArgumentException("Venda não existe no banco de dados");
                }
                else if (await _context.ItemSale.AnyAsync(i => i.SaleId == itens[0].SaleId)
                && itens[0].SaleId != Guid.Empty)
                {
                    int value = 0;
                    List<ItemSale> list = await _context.ItemSale.Where(i => i.SaleId == itens[0].SaleId).ToListAsync();

                    foreach (var item in list)
                    {
                        value = 0;
                        _context.ItemSale.Remove(item);
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
                    if (value == 1)
                    {
                        foreach (ItemSale item in itens)
                        {
                            value = 0;

                            await _context.ItemSale.AddAsync(item);
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
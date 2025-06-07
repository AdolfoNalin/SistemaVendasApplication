using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVendasAplication.Data;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : Controller
    {
        private SysComAppDBContext _context;

        public ProductController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Product> products = await _context.Product.OrderBy(p => p.ShortDescription).ToListAsync()
                ?? throw new ArgumentNullException("Lista está fazia!");

                return Ok(products);
            }
            catch (ArgumentNullException ane)
            {
                return BadRequest(ane.Message);
            }
            catch (Exception ex)
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
                List<Product> products = await _context.Product.Where(p => p.Id.ToString().Contains(id.ToString())).ToListAsync()
                ?? throw new ArgumentNullException("Lista está fazia!");

                return Ok(products);
            }
            catch (ArgumentNullException ane)
            {
                return BadRequest(ane.Message);
            }
            catch (Exception ex)
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
                if (value is null)
                {
                    throw new ArgumentNullException("O valor é nulo!");
                }
                else if (await _context.Product.AnyAsync(p => p.FullDescription.ToUpper().Contains(value.ToUpper())
                || p.ShortDescription.ToUpper().Contains(value.ToUpper())) == false)
                {
                    throw new ArgumentException("Produto não existe!");
                }
                else if (await _context.Product.AnyAsync(p => p.FullDescription.ToUpper().Contains(value.ToUpper())
                || p.ShortDescription.ToUpper().Contains(value.ToUpper())))
                {
                    List<Product> products = await _context.Product.Where<Product>(p => p.FullDescription.ToUpper().Contains(value.ToUpper()) ||
                    p.ShortDescription.ToUpper().Contains(value.ToUpper())).ToListAsync() ?? throw new ArgumentException("Lista está vazia!");

                    return Ok(products);
                }
                else
                {
                    throw new Exception("Aconteceu um erro");
                }

            }
            catch (ArgumentNullException ane)
            {
                return NotFound(ane.Message);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            try
            {
                if (product is null)
                {
                    throw new ArgumentNullException("Produto é nulo");
                }
                else if (await _context.Product.AnyAsync(p => p.FullDescription.ToUpper().Contains(product.FullDescription.ToUpper())
                || p.ShortDescription.ToUpper().Contains(product.ShortDescription.ToUpper())))
                {
                    throw new ArgumentException("Produto já existe no banco de dados");
                }
                else if (await _context.Product.AnyAsync(p => p.FullDescription.ToUpper().Contains(product.FullDescription.ToUpper())
                || p.ShortDescription.ToUpper().Contains(product.ShortDescription.ToUpper())) == false && product != null)
                {
                    await _context.Product.AddAsync(product);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Produto foi cadastrado com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Produto não cadastrado");
                    }
                }
                else
                {
                    return BadRequest("Aconteceu um erro!");
                }
            }
            catch (ArgumentNullException ane)
            {
                return NotFound(ane.Message);
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
        public async Task<IActionResult> Put([FromBody] Product product)
        {
            try
            {
                if (product is null)
                {
                    throw new ArgumentNullException("Produto é nulo");
                }
                else if (await _context.Product.AnyAsync(p => p.FullDescription.ToUpper().Contains(product.FullDescription.ToUpper())
                || p.ShortDescription.ToUpper().Contains(product.ShortDescription.ToUpper())) == false)
                {
                    throw new ArgumentException("Produto não  existe no banco de dados");
                }
                else if (await _context.Product.AnyAsync(p => p.FullDescription.ToUpper().Contains(product.FullDescription.ToUpper())
                || p.ShortDescription.ToUpper().Contains(product.ShortDescription.ToUpper())) && product != null)
                {
                    _context.Product.Update(product);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Produto foi atualizado com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Produto não atualizado");
                    }
                }
                else
                {
                    return BadRequest("Aconteceu um erro!");
                }
            }
            catch (ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
            catch (DbUpdateException ue) when (ue.InnerException.Message.Contains("23505"))
            {
                return BadRequest("Chave primaria duplicada!");
            }
            catch (Exception ex) when (ex.InnerException?.Message.Contains("23505") == true)
            {
                return BadRequest($"{ex.InnerException?.Message},{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region Delete
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Product product)
        {
             if (product is null)
                {
                    throw new ArgumentNullException("Produto é nulo");
                }
                else if (await _context.Product.AnyAsync(p => p.FullDescription.ToUpper().Contains(product.FullDescription.ToUpper())
                || p.ShortDescription.ToUpper().Contains(product.ShortDescription.ToUpper())) == false)
                {
                    throw new ArgumentException("Produto não existe no banco de dados");
                }
                else if (await _context.Product.AnyAsync(p => p.FullDescription.ToUpper().Contains(product.FullDescription.ToUpper())
                || p.ShortDescription.ToUpper().Contains(product.ShortDescription.ToUpper())) && product != null)
                {
                    _context.Product.Remove(product);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Produto foi deletado com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Produto não deletado");
                    }
                }
                else
                {
                    return BadRequest("Aconteceu um erro!");
                }
        }
        #endregion
    }
}
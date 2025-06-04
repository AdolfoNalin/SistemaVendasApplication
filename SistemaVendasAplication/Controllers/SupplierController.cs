using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;
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
    //[Authoraize]
    public class SupplierController : Controller
    {
        private SysComAppDBContext _context;

        public SupplierController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Supplier> suppliers = await _context.Supplier.OrderBy(s => s.Name).ToListAsync()
                ?? throw new ArgumentNullException("Lista é nula");

                return Ok(suppliers);
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
            
                List<Supplier> suppliers = await _context.Supplier.Where<Supplier>(s => s.Id == id).OrderBy(s => s.Name).ToListAsync()
                ?? throw new ArgumentNullException("Lista está vazia");

                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region GetString
        [HttpGet]
        [Route("smart/{value}")]
        public async Task<IActionResult> Get([FromRoute] string value)
        {
            try
            {
                if (value is null || value == String.Empty)
                {
                    throw new ArgumentNullException("Arguemnto nulo");
                }
                else
                {
                    List<Supplier> suppliers = await _context.Supplier.Where<Supplier>(s => s.Name.ToUpper().Contains(value.ToUpper()) || s.ShotName.ToUpper().Contains(value.ToUpper())
                    || s.CNPJ.Contains(value) || s.CPF.Contains(value)).OrderBy(s => s.Name).ToListAsync() ??
                    throw new ArgumentException("Lista está nula");

                    return Ok(suppliers);
                }
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
        public async Task<IActionResult> Post([FromBody] Supplier supplier)
        {
            try
            {
                if (supplier is null)
                {
                    throw new ArgumentNullException("Fornecedor é nulo");
                }
                else if (await _context.Supplier.AnyAsync(s => s.Id.ToString().Contains(supplier.Id.ToString()) || s.CompanyName.ToUpper().Contains(supplier.CompanyName.ToUpper())
                || s.CPF.Contains(supplier.CPF) || s.CNPJ.Contains(supplier.CNPJ)))
                {
                    throw new ArgumentException("Fornecedor existente no banco de dados.");
                }
                else if (await _context.Supplier.AnyAsync(s => s.Id.ToString().Contains(supplier.Id.ToString()) || s.CompanyName.ToUpper().Contains(supplier.CompanyName.ToUpper())
                || s.CPF.Contains(supplier.CPF) || s.CNPJ.Contains(supplier.CNPJ)) == false
                && supplier != null)
                {
                    supplier.DueDate = supplier.DueDate.ToUniversalTime();
                    await _context.Supplier.AddAsync(supplier);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Fornecedor cadastrado com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Fornecedor não foi cadastrado");
                    }
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
            catch (System.Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region Put
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Supplier supplier)
        {
            try
            {
                if (supplier is null)
                {
                    throw new ArgumentNullException("Funcionário é nulo");
                }
                else if (await _context.Supplier.AnyAsync(s => s.Id.ToString().Contains(supplier.Id.ToString()) || s.CompanyName.ToUpper().Contains(supplier.CompanyName.ToUpper())
                || s.CPF.Contains(supplier.CPF) || s.CNPJ.Contains(supplier.CNPJ)) == false)
                {
                    throw new ArgumentException("Funcionário não existente no banco de dados.");
                }
                else if (await _context.Supplier.AnyAsync(s => s.Id.ToString().Contains(supplier.Id.ToString()) || s.CompanyName.ToUpper().Contains(supplier.CompanyName.ToUpper())
                || s.CPF.Contains(supplier.CPF) || s.CNPJ.Contains(supplier.CNPJ))
                && supplier != null)
                {
                    supplier.DueDate = supplier.DueDate.ToUniversalTime();
                    _context.Supplier.Update(supplier);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Fornecedor atualizado com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Forncedor não foi atualizados");
                    }
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
            catch (System.Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region Delete
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Supplier supplier)
        {
            try
            {
                if (supplier is null)
                {
                    throw new ArgumentNullException("Fornecedor nulo");
                }
                else if (await _context.Supplier.AnyAsync(s => s.CPF.Contains(supplier.CEP) || s.CNPJ.Contains(supplier.CNPJ)) == false)
                {
                    throw new ArgumentException($"{supplier.Name} não existe no banco de dados");
                }
                else if (await _context.Supplier.AnyAsync(s => s.CPF.Contains(supplier.CPF) || s.CNPJ.Contains(supplier.CNPJ))
                && supplier != null)
                {
                    _context.Supplier.Remove(supplier);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Fornecedor deletado com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Fornecedor não foi deletado");
                    }
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
    }
}
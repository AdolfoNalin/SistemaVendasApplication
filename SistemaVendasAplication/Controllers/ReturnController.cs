using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.EntityFrameworkCore;
using SistemaVendasAplication.Data;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ReturnController : Controller
    {
        private readonly SysComAppDBContext _context;

        public ReturnController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Return> returns = await _context.Return.ToListAsync()
                ?? throw new ArgumentNullException("Nenhuma devolução foi encontrada!");

                return Ok(returns);
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

        #region GetSmart
        [HttpGet("Smart")]
        public async Task<IActionResult> Get([FromRoute] string value)
        {
            try
            {
                Client client = await _context.Client.Where(c => c.Name.ToUpper().Contains(value.ToUpper()))
                .FirstOrDefaultAsync()
                ?? throw new ArgumentNullException("Client não encontrado!");

                List<Return> returns = await _context.Return.Where(r => r.ClientId == client.Id).ToListAsync()
                ?? throw new ArgumentNullException("Cliente não realizou nenhuma devolução");

                return Ok(returns);
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
        [HttpGet("Date")]
        public async Task<IActionResult> Get([FromQuery] DateTime startDate, DateTime endDate)
        {
            try
            {
                List<Return> returns = await _context.Return.Where(r => r.Date == startDate && r.Date == endDate).ToListAsync()
                ?? throw new ArgumentNullException($"Nenhum devolução foi encontrada da Data: {startDate} á Data: {endDate}");

                return Ok(returns);
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

        #region Post
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Return obj)
        {
            try
            {
                if (obj is null)
                {
                    throw new ArgumentNullException("Preencha todos os campos da devolução");
                }
                else if (await _context.Return.AnyAsync(r => r.Id == obj.Id))
                {
                    throw new ArgumentException("Devolução já existe no banco de dados!");
                }
                else
                {
                    await _context.Return.AddAsync(obj);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Devolução cadastrada com Sucesso!");
                    }
                    else
                    {
                        return BadRequest("Devolução não cadastrada!");
                    }
                }
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.ParamName);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region Put
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Return obj)
        {
            try
            {
                if (obj is null)
                {
                    throw new ArgumentNullException("Preencha todos os campos da devolução");
                }
                else if (await _context.Return.AnyAsync(r => r.Id == obj.Id) == false)
                {
                    throw new ArgumentException("Devolução não existe no banco de dados!");
                }
                else
                {
                    _context.Return.Update(obj);
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

        #region Delete
        [HttpDelete("{returnId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid returnId)
        {
            try
            {
                if (returnId == Guid.Empty)
                {
                    throw new ArgumentNullException("Id não encontrado");
                }
                else if (await _context.Return.AnyAsync(r => r.Id == returnId) == false)
                {
                    throw new ArgumentException("Devolução não existe");
                }
                else
                {
                    Return @return = await _context.Return.Where(r => r.Id == returnId).FirstAsync();
                    _context.Return.Remove(@return);

                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Devolução removida com sucesso");
                    }
                    else
                    {
                        return BadRequest("Devolução não foi removida!");
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
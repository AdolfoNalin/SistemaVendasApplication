using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class CashMovimentController : Controller
    {
        private SysComAppDBContext _context;

        public CashMovimentController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<CashMoviment> moviments = await _context.CashMoviment.Order().ToListAsync()
                ?? throw new ArgumentNullException("Nenhuma movimentação de caixa");

                return Ok(moviments);
            }
            catch(ArgumentNullException ane)
            {
                return NotFound(ane.ParamName);
            }
            catch (System.Exception ex)
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
                List<CashMoviment> moviments = await _context.CashMoviment.Where(c => c.Date == startDate && c.Date == endDate).ToListAsync() ??
                throw new ArgumentNullException("Nenhuma movimentação de caixa encontrado");

                return Ok(moviments);
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

        #region GetUser
        [HttpGet("User/{user}")]
        public async Task<IActionResult> Get([FromRoute] string user)
        {
            try
            {
                List<User> users = await _context.User.Where(u => u.Name.ToUpper().Contains(user.ToUpper()) ||
                u.Login.ToUpper().Contains(user.ToUpper())).ToListAsync()
                ?? throw new ArgumentNullException("Usuário não foi encontrado");

                List<CashMoviment> moviments = await _context.CashMoviment.Where(c => c.UserId == users[0].Id).ToListAsync() ??
                throw new ArgumentNullException("Nenhuma movimentação realizada");

                return Ok(moviments);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.StackTrace}");
            }
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CashMoviment moviment)
        {
            try
            {
                if (moviment is null)
                {
                    throw new ArgumentNullException("Movimentação é vazia");
                }
                else if (await _context.CashMoviment.AnyAsync(c => c.Id == moviment.Id))
                {
                    throw new ArgumentException("Movimento já existe no banco de dados");
                }
                else
                {
                    _context.CashMoviment.Update(moviment);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Movimentação realizada com sucesso");
                    }
                    else
                    {
                        return BadRequest("Movimentação não foi realizada");
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
        [HttpPost]
        public async Task<IActionResult> Put([FromBody] CashMoviment moviment)
        {
            try
            {
                if (moviment is null)
                {
                    throw new ArgumentNullException("Movimentação é vazia");
                }
                else if (await _context.CashMoviment.AnyAsync(c => c.Id == moviment.Id))
                {
                    throw new ArgumentException("Movimento já existe no banco de dados");
                }
                else
                {
                    await _context.CashMoviment.AddAsync(moviment);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Movimentação atualizada com sucesso");
                    }
                    else
                    {
                        return BadRequest("Movimentação não foi atualizada");
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

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException("Movimentação não pode ser nula");
                }
                else if (await _context.CashMoviment.AnyAsync(c => c.Id == id) == false)
                {
                    throw new ArgumentException("Movimentação não existe no banco de dados");
                }
                else
                {
                    CashMoviment moviment = _context.CashMoviment.Where(c => c.Id == id).ToList().First();
                    _context.CashMoviment.Remove(moviment);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Movimentação excluida com sucesso");
                    }
                    else
                    {
                        return BadRequest("Movimentação não foi excluida");
                    }
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion
    }
}
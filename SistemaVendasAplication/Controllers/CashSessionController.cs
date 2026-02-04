using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal; 
using SistemaVendasAplication.Data;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CashSessionController : Controller
    {
        private SysComAppDBContext _context;
        public CashSessionController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<CashSession> descks = await _context.CashSession.OrderBy(c => c.Date).ToListAsync()
                ?? throw new ArgumentNullException("Nenhum Caixa encontrado");

                return Ok(descks);
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

        #region GetId
        [HttpGet("Search/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                CashSession session = _context.CashSession.Where(c => c.Id == id).ToList().First()
                ?? throw new ArgumentNullException("Nenhum Caixa encontrado");

                return Ok(session);
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

        #region GetDate
        [HttpGet("Date")]
        public async Task<IActionResult> Get([FromQuery] DateTime startDate, DateTime endDate)
        {
            try
            {
                DateTime start = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
                DateTime end = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

                List<CashSession> descks = await _context.CashSession.Where(c => c.Date.Date >= start.Date &&
                c.Date.Date <= end.Date).ToListAsync()
                ?? throw new ArgumentNullException("Nenhum Caixa registrado nestas datas");

                return Ok(descks);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region GetEmployee
        [HttpGet("Employee/{value}")]
        public async Task<IActionResult> Get([FromRoute] string value)
        {
            try
            {
                Employee employee = _context.Employee.Where(e => e.Name.ToUpper().Contains(value.ToUpper())
                || e.ShortName.ToUpper().Contains(value.ToUpper())).ToList().First()
                ?? throw new ArgumentNullException("Funcionário não existe");

                User user = _context.User.Where(u => u.EmployeeId == employee.Id).ToList().First()
                ?? throw new ArgumentNullException("Usuário não existe");

                List<CashSession> cashDescks = await _context.CashSession.Where(c => c.UserId == user.Id).ToListAsync()
                ?? throw new ArgumentNullException("Não existe caixa com esse funcionário!");

                return Ok(cashDescks);
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
    
        #region GetEnable
        [HttpGet("Enabel/{enable}")]
        public async Task<IActionResult> GetEnabel([FromRoute] Enable enable)
        {
            try
            {
                List<CashSession> cashs = await _context.CashSession.OrderBy(c => c.Date).ToListAsync()
                ?? throw new ArgumentNullException("Nenhum caixa encontrado");

                cashs.ToList().ForEach(c =>
                
                    c.Enable = c.Enable == Enable.Enable ? Enable.Habilitado : Enable.Desabilitado
                );

                List<CashSession> returnCashs = cashs.Where(c => c.Enable == enable).ToList();

                return Ok(returnCashs);
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

        #region Post
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CashSession cashSession)
        {
            try
            {
                if (cashSession is null)
                {
                    throw new ArgumentNullException("Caixa é nulo");
                }
                else if (await _context.CashSession.AnyAsync(c => c.Id == cashSession.Id))
                {
                    throw new ArgumentException("Caixa já existe no banco de dados");
                }
                else
                {
                    await _context.CashSession.AddAsync(cashSession);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Caixa foi cadastrado com sucesso");
                    }
                    else
                    {
                        return BadRequest("Caixa não foi cadastrado");
                    }
                }
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.ParamName);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        #endregion

        #region Put
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CashSession cashDesck)
        {
            try
            {
                if (cashDesck is null)
                {
                    throw new ArgumentNullException("Caixa é nulo");
                }
                else if (await _context.CashSession.AnyAsync(c => c.Id == cashDesck.Id) == false)
                {
                    throw new ArgumentException("Caixa não existe no banco de dados");
                }
                else
                {
                    _context.CashSession.Update(cashDesck);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Caixa foi atualizado");
                    }
                    else
                    {
                        return BadRequest("Caixa não foi atualizado");
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        #endregion

        #region PutCashMovimente
        [HttpPut("CashMoviment")]
        public async Task<IActionResult> PutCashMoviment([FromBody] CashSession cashSession)
        {
            try
            {
                if(cashSession is null)
                {
                    throw new ArgumentNullException("Essa movimentação de caixa não é valida");
                }
                else if(await _context.CashSession.AnyAsync(c => c.Id == cashSession.Id) == false)
                {
                    throw new ArgumentException("Movimentação de caixa não existe");
                }
                else
                {
                    _context.CashSession.Update(cashSession);
                    int value = await _context.SaveChangesAsync();

                    if(value == 1)
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest(false);
                    }
                }
            }
            catch(ArgumentException ane)
            {
                return NotFound(ane.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region Disabel
        [HttpPut("Disable")]
        public async Task<IActionResult> Disable([FromBody] CashSession cash)
        {
            if (cash is null)
            {
                throw new ArgumentNullException("Id é vazio");
            }
            else if (await _context.CashSession.AnyAsync(c => c.Id == cash.Id) == false)
            {
                throw new ArgumentException("Caixa não existe no banco de dados");
            }
            else
            {
                _context.CashSession.Update(cash);
                int value = await _context.SaveChangesAsync();

                if (value == 1)
                {
                    return Ok("Caixa deletado com sucesso");
                }
                else
                {
                    return BadRequest("Caixa não foi deletado");
                }
            }
        }
        #endregion
    }
}
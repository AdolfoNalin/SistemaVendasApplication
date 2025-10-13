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
using SistemaVendasAplication.Migrations;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CashDesckController : Controller
    {
        private SysComAppDBContext _context;
        public CashDesckController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<CashDesck> descks = await _context.CashDescks.ToListAsync()
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

        #region GetDate
        [HttpGet("Smart")]
        public async Task<IActionResult> Get([FromQuery] DateTime startDate, DateTime endDate)
        {
            try
            {
                List<CashDesck> descks = await _context.CashDescks.Where(c => c.Date == startDate && c.Date == endDate).ToListAsync()
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

                List<CashDesck> cashDescks = await _context.CashDescks.Where(c => c.UserId == user.Id).ToListAsync()
                ?? throw new ArgumentNullException("Não existe caixa com esse funcionário!");

                return Ok(cashDescks);
            }
            catch (ArgumentNullException ane)
            {
                return NotFound(ane.ParamName);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CashDesck cashDesck)
        {
            try
            {
                if (cashDesck is null)
                {
                    throw new ArgumentNullException("Caixa é nulo");
                }
                else if (await _context.CashDescks.AnyAsync(c => c.Id == cashDesck.Id))
                {
                    throw new ArgumentException("Caixa já existe no banco de dados");
                }
                else
                {
                    await _context.CashDescks.AddAsync(cashDesck);
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
        public async Task<IActionResult> Put([FromBody] CashDesck cashDesck)
        {
            try
            {
                if (cashDesck is null)
                {
                    throw new ArgumentNullException("Caixa é nulo");
                }
                else if (await _context.CashDescks.AnyAsync(c => c.Id == cashDesck.Id) == false)
                {
                    throw new ArgumentException("Caixa não existe no banco de dados");
                }
                else
                {
                    _context.CashDescks.Update(cashDesck);
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

        #region Delete
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid cashId)
        {
            if (cashId == Guid.Empty)
            {
                throw new ArgumentNullException("Id é vazio");
            }
            else if (await _context.CashDescks.AnyAsync(c => c.Id == cashId) == false)
            {
                throw new ArgumentException("Caixa não existe no banco de dados");
            }
            else
            {
                CashDesck cashDesck = _context.CashDescks.Where(c => c.Id == cashId).ToList().First();
                _context.CashDescks.Remove(cashDesck);
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
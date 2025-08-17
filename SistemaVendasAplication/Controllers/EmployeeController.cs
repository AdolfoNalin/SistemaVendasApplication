using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVendasAplication.Data;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private SysComAppDBContext _context;

        public EmployeeController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Employee> employees = await _context.Employee.OrderBy(e => e.Name).ToListAsync() 
                    ?? throw new ArgumentNullException("Lista está nula");

                return Ok(employees);
            }
            catch(ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region GetId
        [HttpGet]
        [Route("search/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                Employee employee = await _context.Employee.Where(e => e.Id == id).FirstOrDefaultAsync()
                ?? throw new ArgumentNullException("");

                return Ok(employee);
            }
            catch(ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetString
        [HttpGet("smart/{value}")]
        public async Task<IActionResult> Get([FromRoute] string value)
        {
            try
            {
                List<Employee> employees = await _context.Employee.Where<Employee>(e => e.Name.ToUpper().Contains(value.ToUpper()) || e.ShortName.ToUpper().Contains(value.ToUpper())
                || e.CPF.Contains(value)).ToListAsync()
                ?? throw new ArgumentNullException("Nenhum Funcionário encontrado!");

                return Ok(employees);
            }
            catch(ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
                throw;
            }
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            try
            {
                if (employee is null)
                {
                    throw new ArgumentNullException("Funcionário não pode ser nulo!");
                }
                else if (await _context.Employee.AnyAsync(e => e.CPF.Contains(employee.CPF)))
                {
                    throw new ArgumentException("Funcionário já existente no banco de dados!");
                }
                else if (employee != null && await _context.Employee.AnyAsync(e => e.CPF.Contains(employee.CPF)) == false)
                {
                    employee.DueDate = employee.DueDate.ToUniversalTime();
                    await _context.Employee.AddAsync(employee);
                    int value = await _context.SaveChangesAsync();
                    
                    if(value  == 1)
                    {
                        return Ok("Funcionário foi cadastrado com Sucesso!");
                    }
                    else
                    {
                        return BadRequest("Funcionário não cadastrado");
                    }
                }
                else
                {
                    throw new Exception("Aconteceu um erro!");
                }
            }
            catch(ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
            catch(ArgumentException ae)
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
        public async Task<IActionResult> Put([FromBody] Employee employee)
        {
            try
            {
                if (employee is null)
                {
                    throw new ArgumentNullException("Funcioário não pode ser nulo.");
                }
                else if (await _context.Employee.AnyAsync(e => e.CPF == employee.CPF) == false)
                {
                    throw new ArgumentException("Funcionário não existente no banco de dados.");
                }
                else if (await _context.Employee.AnyAsync(e => e.CPF == employee.CPF) && employee != null) 
                {
                    employee.DueDate = employee.DueDate.ToUniversalTime();
                    _context.Employee.Update(employee);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Funcionário atualizado com sucesso");
                    }
                    else
                    {
                        return BadRequest("Funcionário não foi cadastrado");
                    }
                }
                else
                {
                    throw new Exception("Aconteceu um erro");
                }
            }
            catch(ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
            catch(ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
            catch (Exception ex)
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
                if (id.ToString() == null || id.ToString() == String.Empty)
                {
                    throw new ArgumentNullException("Id do Funcionário é nulo ou está vazio");
                }
                else if (await _context.Employee.AnyAsync(e => e.Id.ToString().Contains(id.ToString())) == false)
                {
                    throw new ArgumentException("Fucionário não existe");
                }
                else if (await _context.Employee.AnyAsync(e => e.Id.ToString().Contains(id.ToString())))
                {
                    Employee employee = await _context.Employee.Where(e => e.Id.ToString().Contains(id.ToString())).FirstOrDefaultAsync()
                    ?? throw new ArgumentNullException("Funcionário não encontrado");

                    _context.Employee.Remove(employee);
                    
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Funcionário foi deletado com sucesso");
                    }
                    else
                    {
                        return BadRequest("Funcionário não deletado!");
                    }
                }
                else
                {
                    throw new Exception("Aconteceu um erro");
                }
            }
            catch(ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SistemaVendasAplication.Data;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private SysComAppDBContext _context;

        public UserController(SysComAppDBContext context)
        {
            _context = context;
        }

        #region GetAll
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<User> users = await _context.User.OrderBy(u => u.Name).ToListAsync()
                ?? throw new ArgumentNullException("Nenhum usuário encontrado!");

                return Ok(users);
            }
            catch (ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        #endregion

        #region GetId
        [HttpGet("Search/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                User user = await _context.User.Where(u => u.Id.ToString().Contains(id.ToString())).FirstOrDefaultAsync()
                ?? throw new ArgumentNullException("Nenhum usuário encontrado!");

                return Ok(user);
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

        #region GetSmart
        [HttpGet("Smart/{value}")]
        public async Task<IActionResult> Get([FromRoute] string value)
        {
            try
            {
                User user = await _context.User.Where(u => u.Name.ToUpper().Contains(value.ToUpper()) ||
                u.Login.ToUpper().Contains(value.ToUpper())).FirstOrDefaultAsync()
                ?? throw new ArgumentNullException("Nenhum usuário encontrado");

                return Ok(user);
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

        #region Post
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                if (user is null)
                {
                    throw new ArgumentNullException("Usuário não pode ser cadastrado. Porque é nulo");
                }
                else if (await _context.User.AnyAsync(u => u.Id.ToString().Contains(user.Id.ToString()) ||
                u.Name.ToUpper().Contains(user.Name.ToUpper())))
                {
                    throw new ArgumentException("Usuário já existe no banco de dados");
                }
                else if (await _context.User.AnyAsync(u => u.Id.ToString().Contains(user.Id.ToString()) ||
                u.Name.ToUpper().Contains(user.Name.ToUpper())) == false && user != null)
                {
                    await _context.User.AddAsync(user);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Usuário foi cadastrado com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Usuário não foi cadastrado!");
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
            catch (System.Exception ex)
            {
                return BadRequest($"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}");
            }
        }
        #endregion

        #region Put
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            try
            {
                if (user is null)
                {
                    throw new ArgumentNullException("Usuário não pode ser Atualizado porque é nulo");
                }
                else if (await _context.User.AnyAsync(u => u.Id.ToString().Contains(user.Id.ToString())) == false)
                {
                    throw new ArgumentException("Usuário não pode ser atualizado. Não existe no banco de dados");
                }
                else if (await _context.User.AnyAsync(u => u.Id.ToString().Contains(user.Id.ToString())) && user != null)
                {
                    _context.User.Update(user);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Usuário foi atualizado com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Usuário não foi atualziado");
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                if (id.ToString() is null || id.ToString() == String.Empty)
                {
                    throw new ArgumentNullException("Usuário não pode ser Deletado porque é nulo");
                }
                else if (await _context.User.AnyAsync(u => u.Id.ToString().Contains(id.ToString())) == false)
                {
                    throw new ArgumentException("Usuário não pode ser Deletado. Não existe no banco de dados");
                }
                else if (await _context.User.AnyAsync(u => u.Id.ToString().Contains(id.ToString())) &&
                id.ToString() != null)
                {
                    User user = await _context.User.FirstAsync(u => u.Id.ToString().Contains(id.ToString()));
                    _context.User.Remove(user);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Usuário foi deletado com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Usuário não foi deletado");
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
    }
}
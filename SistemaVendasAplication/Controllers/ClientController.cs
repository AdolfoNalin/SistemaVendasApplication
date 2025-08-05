using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SistemaVendasAplication.Data;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientController : Controller
    {
        private SysComAppDBContext _context;

        public ClientController(SysComAppDBContext context)
        {
            _context = context;
        }
 
        #region Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Client> clients = await _context.Client.OrderBy(c => c.Name).ToListAsync()
                ?? throw new ArgumentNullException("Não existe nenhum client");

                return Ok(clients);
            }
            catch (ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
        }
        #endregion

        #region GetId
        [HttpGet("search/{id}")]
        public async Task<IActionResult> GetId([FromRoute] Guid id)
        {
            try
            {
                Client client = await _context.Client.Where<Client>(c => c.Id == id).FirstOrDefaultAsync()
                ?? throw new ArgumentNullException("Cliente não existe!");

                return Ok(client);
            }
            catch (ArgumentNullException ane)
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
        [HttpGet]
        [Route("smart/{value}")]
        public async Task<IActionResult> Get([FromRoute] string value)
        {
            try
            {
                Client client = await _context.Client.Where<Client>(c => c.Name.ToUpper().Contains(value.ToUpper()) || c.ShotName.ToUpper().Contains(value.ToUpper()) 
                || c.CPF.Contains(value)).FirstOrDefaultAsync()
                ?? throw new ArgumentNullException("Cliente não encontrado!");

                return Ok(client);
            }
            catch (ArgumentNullException ane)
            {
                return NotFound(ane.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Client client)
        {
            try
            {
                if (client is null)
                {
                    throw new ArgumentNullException("O cliente não pode ser nulo!");
                }
                else if (await _context.Client.AnyAsync(c => c.RG.Contains(client.RG)))
                {
                    throw new ArgumentException("Cliente já existe no banco de dados! Verifique o RG");
                }
                else if (await _context.Client.AnyAsync(c => c.CPF.Contains(client.CPF)))
                {
                    throw new ArgumentException("Cliente já existente no banco dados! Verifique o CPF");
                }
                else if (client != null && await _context.Client.AnyAsync(c => c.Id.ToString().Contains(client.Id.ToString()) || c.RG.Contains(client.RG) || c.CPF.Contains(client.CPF)) == false)
                {
                    client.DueDate = client.DueDate.ToUniversalTime();
                    await _context.Client.AddAsync(client);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Cliente cadastrado com Sucesso!");
                    }
                    else
                    {
                        return BadRequest("cliente não cadastrado");
                    }
                }
                else
                {
                    throw new Exception("Aconteceu um erro");
                }

            }
            catch (DbUpdateException ue)
            {
                return BadRequest($"{ue.Message}, {ue.StackTrace}, {ue.HelpLink}"); 
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

        #region Put
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Client client)
        {
            try
            {
                if (client is null)
                {
                    throw new ArgumentNullException("Cliente nulo!");
                }
                else if (await _context.Client.AnyAsync(c => c.Id.ToString().Contains(client.Id.ToString()) || c.RG.Contains(client.RG) || c.CPF.Contains(client.CPF)))
                {
                    client.DueDate = client.DueDate.ToUniversalTime();
                    _context.Client.Update(client);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok(new { message = "Cliente foi atualizado com sucesso!" });
                    }
                    else
                    {
                        return BadRequest("Cliente não foi atualizado!");
                    }
                }
                else
                {
                    throw new Exception("Aconteceu um erro!");
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

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                if (id.ToString() == null || id.ToString() == String.Empty)
                {
                    throw new ArgumentNullException("Id é nulo!");
                }
                else if (await _context.Client.AnyAsync(c => c.Id.ToString().Contains(id.ToString())) == false)
                {
                    throw new ArgumentException("Nenhum resultado encontrado!");
                }
                else if (await _context.Client.AnyAsync(c => c.Id.ToString().Contains(id.ToString())))
                {
                    Client client = await _context.Client.FirstAsync(c => c.Id.ToString().Contains(id.ToString()));
                    _context.Client.Remove(client);
                    int value = await _context.SaveChangesAsync();

                    if (value == 1)
                    {
                        return Ok("Cliente deletado com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Cliente não foi deletado!");
                    }
                }
                else
                {
                    throw new Exception("Aceonteceu um erro");
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
    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Service
{
    public class TokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GeneratorToken
        public string GeneratorToken(User user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["key"] ?? throw new ArgumentNullException("Chave não encontrada"));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                         new Claim[]
                         {
                        new Claim(ClaimTypes.NameIdentifier, user.Login.ToString()),
                        new Claim(ClaimTypes.Role, user.EmployeeId.ToString()),
                         }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                return $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}";
            }
        }
        #endregion
    }
}
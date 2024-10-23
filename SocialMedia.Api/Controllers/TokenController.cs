using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ISeguridadService _seguridadService;

        private readonly IPasswordService _passwordService;
        public TokenController(IConfiguration configuration, ISeguridadService seguridadService,IPasswordService passwordService) { 
        
            _configuration = configuration;
            _seguridadService= seguridadService;
            _passwordService= passwordService;
            
        }

        [HttpPost]
        public async Task<IActionResult> TokenAuthentification(UserLogin userLoging)
        {
            //if it is  a user valid
            var Validations= await IsValidUser(userLoging);
            if (Validations.Item1)
            {
                var Token = GenerationsToken(Validations.Item2!);
                return Ok(new { Token });
            }
            return NotFound();
        }

        private async Task<(bool,Seguridad?)> IsValidUser(UserLogin userLogin)
        {
               var User= await _seguridadService.GetLoginByCredencial(userLogin);
               var isValid = _passwordService.Check(User.Password, userLogin.Password);
               return (isValid,User);
              
        }

        private string GenerationsToken(Seguridad seguridad)
        {
            //header
            var SymmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentification:SecretKey"]!));
            var Credencials=new SigningCredentials(SymmetricSecurity,SecurityAlgorithms.HmacSha256);
            var Header= new JwtHeader(Credencials);

            //Claims
            var Claims = new[]
            {
                new  Claim(ClaimTypes.Name,seguridad.UserName),
                new  Claim("User",seguridad.User),
                new  Claim(ClaimTypes.Role,seguridad.Role.ToString()),

            };
            var Payload = new JwtPayload
            (
                _configuration["Authentification:Issuer"]!,
                _configuration["Authentification:Audience"]!,
                Claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(10)




            );
            var jwtToken = new JwtSecurityToken(Header, Payload);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}

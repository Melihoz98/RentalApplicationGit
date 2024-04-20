using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentalService.DTO;
using RentalService.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RentalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDTO request)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Email = request.Email;
            user.PasswordHash = passwordHash;

            return Ok(user);
        }


        [HttpPost("login")]
        public ActionResult<User> Login(UserDTO request)
        {
           if(user.Email != request.Email)
            {
                return BadRequest("User not found.");
            }
           if(!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        //Token creation

        //Create token with user object
        private string CreateToken (User user)
        {
            //Need user objects Email to set the email to the token
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            //wrap the key from appsetting
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            //credintials are needed with the key and signature
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // the return of a JSON Token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials : creds
                );

            //write the token
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}

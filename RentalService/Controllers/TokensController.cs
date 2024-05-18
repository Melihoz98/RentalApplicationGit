
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentalService.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RentalService.Controllers
{

    public class TokensController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        // Fetches configuration from more sources
        public TokensController(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
        }

        [Route("token")]
        [HttpPost]
        // Generate and return a JWT token
        public IActionResult Create(string username, string password, string grant_type)
        {

            IActionResult foundToken;
            bool hasInput = ((!string.IsNullOrWhiteSpace(username)) && (!string.IsNullOrWhiteSpace(password)));
            // Only return JWT token if credentials are valid
            SecurityHelper secUtil = new SecurityHelper(_configuration);
            if (hasInput && secUtil.IsValidUsernameAndPassword(username, password))
            {
                RoleEnum role = secUtil.GetRoleEnum(grant_type);
                string jwtToken = GenerateToken(username, role);
                foundToken = new ObjectResult(jwtToken);
            }
            else
            {
                foundToken = BadRequest();
            }
            return foundToken;


        }

        private string GenerateToken(string username, RoleEnum authorRole)
        {
            string jwtString;
            SecurityHelper secUtil = new SecurityHelper(_configuration);

            // Create SigningCredentials for JWT header with algorithm and token type - and secret added
            SymmetricSecurityKey? SIGNING_KEY = secUtil.GetSecurityKey();
            SigningCredentials credentials = new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);

            // Prepare more values for JWT
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, authorRole.ToString())
            };
            int durationInMinutes = 10;
            DateTime expireAt = DateTime.Now.AddMinutes(durationInMinutes);

            // Create a JwtSecurityToken incl. payload and header (uses signing key)
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7023",
                audience: "https://localhost:7023",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            jwtString = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtString;
        }

    }

}

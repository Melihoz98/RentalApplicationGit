using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentalService.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RentalService.Controllers
{
    public class TokensController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokensController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("/token")]
        [HttpPost]
        public IActionResult Create(string username, string password, string grant_type)
        {
            

            string jwtToken = GenerateToken(username);
            return Ok(new { token = jwtToken });
        }

        private string GenerateToken(string username)
        {
            SecurityHelper secUtil = new SecurityHelper(_configuration);

            SymmetricSecurityKey SIGNING_KEY = secUtil.GetSecurityKey();
            SigningCredentials credentials = new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, username),
                
            };

            
            DateTime expiresAt = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7023",
                audience: "https://localhost:7023",
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

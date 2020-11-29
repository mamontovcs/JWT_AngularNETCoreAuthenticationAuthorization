using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SolutionForAuth.Models;

namespace SolutionForAuth.Controllers
{
    public class AuthController : Controller
    {
        private readonly IOptions<AuthOptions> authOptions;

        public AuthController(IOptions<AuthOptions> authOptions)
        {
            this.authOptions = authOptions;
        }

        private List<Account> Accounts => new List<Account>
        {
            new Account()
            {
                Id = 4564565324324,
                Email = "user1@gmail.com",
                Password = "user1",
                Roles = new Role[]{Role.User}
            },
            new Account()
            {
                Id = 25353245324523,
                Email = "user2@gmail.com",
                Password = "user2",
                Roles = new Role[]{Role.User}
            },
            new Account()
            {
                Id = 63424534253242,
                Email = "admin@gmail.com",
                Password = "admin",
                Roles = new Role[]{Role.Admin}
            },
        };

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Login request)
        {
            var user = AuthenticateUser(request.Email, request.Password);

            if (user != null)
            {
                var token = GenerateJWT(user);

                return Ok(new
                {
                    access_token = token
                });
            }

            return Unauthorized();
        }

        private string GenerateJWT(Account user)
        {
            var authParams = authOptions.Value;

            var securityKey = authParams.GetSymetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                 new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim("role", role.ToString()));
            }

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience, claims, expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Account AuthenticateUser(string email, string password)
        {
            return Accounts.SingleOrDefault(x => x.Email == email && x.Password == password);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

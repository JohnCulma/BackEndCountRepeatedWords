using CountRepeatedWords.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CountRepeatedWords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountsController(UserManager<IdentityUser> userManager, 
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseAuthenticatecDTO>> Register(UserCredentialDTO userCredentialDTO)
        {

            var user = new IdentityUser { UserName = userCredentialDTO.Email, 
                Email = userCredentialDTO.Email };
            var result = await userManager.CreateAsync(user, userCredentialDTO.Password);

            if (result.Succeeded) 
            {
                return CreateToken(userCredentialDTO);
            }
            else
            {
                return BadRequest(result.Errors);
            }      
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseAuthenticatecDTO>> Login(UserCredentialDTO userCredentialDTO)
        {
            var result = await signInManager.PasswordSignInAsync(userCredentialDTO.Email,
                userCredentialDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
             {
                return CreateToken(userCredentialDTO);
            }
            else 
            {
                return BadRequest("Login Fail");
            }
        }
        private ResponseAuthenticatecDTO CreateToken(UserCredentialDTO userCredentialDTO)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userCredentialDTO.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["KeyJwt"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
               expires: expiration, signingCredentials: cred);

            return new ResponseAuthenticatecDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration
            };
        }
    }
}

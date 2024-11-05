using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<IdentityUser> userManager, IConfiguration _configuration)
        {
            this.userManager = userManager;
            configuration = _configuration;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser();
                user.Email = register.Email;
                user.UserName = register.UserName;
                IdentityResult result = await userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {

                    return Ok("Account add ");
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest();          
        }
        [HttpPost("Login")]
        public async Task<IActionResult> login(LoginDto login)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(login.UserName);
                if (user != null)
                {
                    bool exist = await userManager.CheckPasswordAsync(user, login.Password);
                    if (exist)
                    {
                        var clims = new List<Claim>();
                        clims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        clims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        clims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        // Add User Role
                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var item in roles)
                        {
                            clims.Add(new Claim(ClaimTypes.Role, item));
                        }


                        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

                        SigningCredentials signing = new SigningCredentials(
                                key, SecurityAlgorithms.HmacSha256
                            );

                        JwtSecurityToken jwt = new JwtSecurityToken(
                            issuer: configuration["JWT:ValidIssuer"],
                            audience: configuration["JWT:ValidAudiance"],
                            claims: clims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signing
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(jwt),
                            expiration = jwt.ValidTo
                        });
                    }

                }
                return Unauthorized();
            }
            return Unauthorized();
        }   
    }
}

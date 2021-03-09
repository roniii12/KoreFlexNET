using DAL;
using KoreFlex.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KoreFlex
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Identity.Application")]
    //[TypeFilter(typeof(AuthorizeJwt))]
    public class AccountController : ControllerBase
    {
        private SignInManager<User> signinManager;
        private UserManager<User> userManager;
        private IConfiguration configuration;
        private ILogger<AccountController> logger;
        public AccountController(SignInManager<User> mgr,
                 UserManager<User> usermgr, IConfiguration config,
                 ILogger<AccountController> logger)
        {
            signinManager = mgr;
            userManager = usermgr;
            configuration = config;
            this.logger = logger;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountController>/5
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Credentials creds)
        {
            logger.LogWarning("hello");
            logger.LogWarning("warning",creds);
            logger.LogInformation("information", creds);
            logger.LogDebug("debug", creds);
            logger.LogTrace("trace", creds);
            var sig = await signinManager.PasswordSignInAsync(creds.Username, creds.Password, false, false); ;
            if (await CheckPassword(creds))
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                byte[] secret = Encoding.ASCII.GetBytes(configuration["jwtKeys:JwtSecret"]);
                SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, creds.Username)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(secret),
                            SecurityAlgorithms.HmacSha256Signature)
                };
                SecurityToken token = handler.CreateToken(descriptor);
                //System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                //mail.To.Add("ron.ziyoni@hot.net.il");
                //mail.To.Add("ronziyoni@gmail.com");
                //mail.From = new MailAddress("test@test.net", "MyName", System.Text.Encoding.UTF8);
                //mail.Subject = "This mail is send from asp.net application";
                //mail.SubjectEncoding = System.Text.Encoding.UTF8;
                //mail.Body = "This is Email Body Text";
                //mail.BodyEncoding = System.Text.Encoding.UTF8;
                //mail.IsBodyHtml = true;
                //mail.Priority = MailPriority.High;
                //SmtpClient client = new SmtpClient();
                //client.Credentials = new System.Net.NetworkCredential("ronziyoni@gmail.com", "ROnman45456789");
                //client.Port = 587;
                //client.Host = "smtp.gmail.com";
                //client.EnableSsl = true;
                //try
                //{
                //    client.Send(mail);
                //}
                //catch (Exception ex)
                //{
                //    Exception ex2 = ex;
                //    string errorMessage = string.Empty;
                //    while (ex2 != null)
                //    {
                //        errorMessage += ex2.ToString();
                //        ex2 = ex2.InnerException;
                //    }
                //}

                return Ok(new
                {
                    success = true,
                    token = handler.WriteToken(token),
                });
            }
            return Unauthorized();
        }
        [HttpPost("logout")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Logout()
        {
            //JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            //SecurityToken sa = new SecurityTokenDescriptor();
            return Ok(new { user = User.Identity.Name});
            await signinManager.SignOutAsync();
            return Ok();
        }
        // POST api/<AccountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccountController>/5
        [HttpPut("Register")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //[Authorize(AuthenticationSchemes = "Identity.Application")]
        //[TypeFilter(typeof(AuthorizeJwt))]
        public async Task<IActionResult> Put([FromBody] Credentials creds)
        {   
            User user = new User() { Email = creds.Username, UserName = creds.Username };
            IdentityResult newUser = await userManager.CreateAsync(user, creds.Password);
            if (newUser.Succeeded)
                return Ok(new { user = newUser });
            return Unauthorized(new { user = User.Identity });
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        private async Task<bool> CheckPassword(Credentials creds)
        {
            User user = await userManager.FindByNameAsync(creds.Username);
            if (user != null)
            {
                foreach (IPasswordValidator<User> v in
                         userManager.PasswordValidators)
                {
                    if ((await v.ValidateAsync(userManager, user,
                            creds.Password)).Succeeded)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public class Credentials
        {
            [Required]
            public string Username { get; set; }
            [Required]
            public string Password { get; set; }
        }
    }
}

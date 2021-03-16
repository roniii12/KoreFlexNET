using BusinessLogic;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KoreFlex.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private UserLogic userLogic;
        private UserManager<User> userManager;
        public AdminController(UserLogic userLogic,
            UserManager<User> userManager)
        {
            this.userLogic = userLogic;
            this.userManager = userManager;
        }
        // GET: api/<AdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AdminController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AdminController>
        [HttpPost("resetIp")]
        public async Task<IActionResult> ResetIp([FromBody] string userId)
        {
            try
            {
                User user = await userManager.FindByIdAsync(userId);
                userLogic.UpdateIpOfUser(user.UserName, null);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }

        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

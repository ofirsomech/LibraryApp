using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Newtonsoft.Json;
using Server.Services.Classes;
using Server.Services.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userService.GetUsersAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/User/create
        [HttpPost("Create")]
        public async Task<ActionResult<bool>> CreateUser([FromBody] string json)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<User>(json);
                var success = await _userService.CreateUserAsync(user);
                if (success)
                    return Ok();
                else
                    return BadRequest();

            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return NotFound();
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody]string json)
        {
            try
            {
                var tmpUser = JsonConvert.DeserializeObject<User>(json);
                var user = await _userService.LoginAsync(tmpUser.Username, tmpUser.Password);
                if (user != null)
                    return Ok(user);
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return NotFound();
            }
        }
    }
}

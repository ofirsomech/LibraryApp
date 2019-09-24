using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Newtonsoft.Json;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        LibraryDbContext context = new LibraryDbContext();
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            try
            {
                //context.Users.Add(new User { Username = "test", Password = "1234", Type = UserTypes.Admin });
                //   await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return new string[] { "value1", "value2" };
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpGet("getid")]
        public ActionResult<int> GetId()
        {
            int nextId = context.Users.Count() + 1;
            return Ok(nextId);
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] string json)
        {
            try
            {
                User user = JsonConvert.DeserializeObject<User>(json);
                //user.Id = context.Users.Count() + 1;
                context.Users.Add(user);
                await context.SaveChangesAsync();
                Console.WriteLine("done");
            }
            catch (Exception err)
            {
                Console.WriteLine(err);

            }
        }



        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

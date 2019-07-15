using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.API.Data;

namespace SocialMediaApp.API.Controllers
{
    // The name of the route is determined by the name of the controller. In this example:
    // http://localhost:5000/api/values/5
    [Route("api/[controller]")]
    [ApiController]

    // The differnce between the Controller class and the ControllerBase class is that the former has MVC view support whilst the latter does not.
    public class ValuesController : ControllerBase
    {
        // In order to access the data inside the Values Controller, we need to inject the DataContext via a constructor
        // We give it the same name as the contoller
        // We pass DataContext as a parameter, give it the name of context.
        // Then we initialize field from paramter so it is available throughout the class and rename it _context.
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;

        }

        // GET api/values
        // IActionResult allows us to return HTTP responses to the client.
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _context.Values.ToListAsync();

            return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(val => val.Id == id);

            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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

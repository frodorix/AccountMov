using CORE.Account.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountMovAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        public readonly IClientesService clientesService;
        public MovimientosController(IClientesService clientesService)
        {
            this.clientesService = clientesService;
        }
        // GET: api/<MovimientosController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MovimientosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MovimientosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MovimientosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MovimientosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

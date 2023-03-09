using CORE.Account.Application.Interfaces;
using CORE.Account.Domain.Model;
using CORE.Account.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountMovAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesService clientes;
        public ClientesController(IClientesService clientes)
        {
            this.clientes = clientes;
        }
        // GET: api/<ClientesController>
        [HttpGet]
        public async Task<IEnumerable<DCliente>> Get(string nombre)
        {
            var clientes = await this.clientes.ObtenerClientes(nombre);
            return clientes;
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClientesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

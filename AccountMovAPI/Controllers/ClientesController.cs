using CORE.Account.Application.Interfaces;
using CORE.Account.Domain.Model;
using CORE.Account.DTO;
using CORE.Account.Exception;
using Infrastructure.Persistence.Entity.Accounts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountMovAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesService clientesService;

       

        public ClientesController(IClientesService clientes)
        {
            this.clientesService = clientes;
        }
        // GET: api/<ClientesController>
        [HttpGet]
        public async Task<IEnumerable<DCliente>> Get(string nombre)
        {
            var clientes = await this.clientesService.ObtenerClientes(nombre);
            return clientes;
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = await this.clientesService.ObtenerPorId(id);
            if (cliente == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(cliente, new StringEnumConverter());
            return Ok(json);
        }

        // POST api/<ClientesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string json)
        {
            
            var cliente = JsonConvert.DeserializeObject<MCliente>(json);
            if (cliente == null) return BadRequest();
            try
            {
                cliente = await this.clientesService.Crear(cliente);
                var response = JsonConvert.SerializeObject(cliente, new StringEnumConverter());
                return StatusCode(201,response);
            }
            catch (ClienteException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            
            
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string json)
        {
            var cliente = JsonConvert.DeserializeObject<MCliente>(json);
            if (cliente == null) return BadRequest();
            try
            {
                var modificados = await this.clientesService.Modificar(cliente);                
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {            
            try
            {
                _= await this.clientesService.Eliminar(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}

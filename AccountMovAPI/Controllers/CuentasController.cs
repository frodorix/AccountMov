using CORE.Account.Application.Interfaces;
using CORE.Account.Domain.Model;
using CORE.Account.Exception;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WebAPI.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountMovAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentasService cuentasService;
        public CuentasController(ICuentasService cuentasService)
        {
            this.cuentasService = cuentasService;  
        }
        // GET: api/<CuentasController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CuentasController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = await this.cuentasService.ObtenerPorId(id);
            if (cliente == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(cliente, new StringEnumConverter());
            return Ok(json);
        }


        // POST api/<CuentasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string json)
        {
            var cuenta = JsonConvert.DeserializeObject<MCuenta>(json);
            if(cuenta == null) return BadRequest("JSon Invalido");
            try
            {
                
                cuenta = await this.cuentasService.Crear(cuenta);
                var response = JsonConvert.SerializeObject(cuenta, new StringEnumConverter());
                return StatusCode(201, response);
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

        // PUT api/<CuentasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string json)
        {
            DCuenta cuenta = JsonConvert.DeserializeObject<DCuenta>(json);
            if (cuenta == null) return BadRequest();
            try
            {
                var modificados = await this.cuentasService.Modificar(cuenta.id, cuenta.estado);
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

        // DELETE api/<CuentasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _ = await this.cuentasService.Eliminar(id);
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

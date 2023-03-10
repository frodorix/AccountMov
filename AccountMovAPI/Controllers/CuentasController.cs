using AccountMovAPI.DTO;
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
        private readonly ILogger<CuentasController> _logger;
        public CuentasController(ICuentasService cuentasService, ILogger<CuentasController> _logge)
        {
            this.cuentasService = cuentasService;  
            this._logger = _logge;
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
        public async Task<IActionResult> Post([FromBody] DNuevaCuenta json)
        {
            if(json == null) return BadRequest("JSon Invalido");
            try
            {
                
                var nueva = await this.cuentasService.Crear(json.ToMCuenta());
                var response = JsonConvert.SerializeObject(nueva.NumeroCuenta, new StringEnumConverter());
                return StatusCode(201, response);
            }
            catch (ClienteException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(500);
            }


        
        }

        // PUT api/<CuentasController>/5
        [HttpPut("{numeroCuenta}")]
        public async Task<IActionResult> Put(int numeroCuenta, [FromBody] DCuentaModificable cuenta)
        {
            
            if (cuenta == null) return BadRequest();
            try
            {
                var modificados = await this.cuentasService.Modificar(numeroCuenta, cuenta.Estado);
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

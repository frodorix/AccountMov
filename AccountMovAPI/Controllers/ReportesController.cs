using AccountMovAPI.DTO;
using CORE.Account.Application.Interfaces;
using CORE.Account.Domain.Model;
using CORE.Account.DTO;
using CORE.Account.Exception;
using Infrastructure.Persistence.Entity.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountMovAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {

        private readonly IClientesService clientesService;
        private readonly ILogger<ReportesController> _logger;

        public ReportesController(IClientesService clientes, ILogger<ReportesController> _logger)
        {
            this.clientesService = clientes;
            this._logger = _logger;
        }

        // GET: api/<ReportesController>
        [HttpGet("{id}/estadocuenta")]
        public async Task<IActionResult> GetEstadoCuenta(int id, DateTime inicio , DateTime fin)
        {
            string json;
            try
            {
                var estado = await this.clientesService.ObtenerEstadoCuenta(id, inicio, fin);
                 json = JsonConvert.SerializeObject(estado, new StringEnumConverter());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
            
            return StatusCode(200, json);
        }

        
    }
}

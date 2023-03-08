using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountMovAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        // GET: api/<ReportesController>
        [HttpGet("estadocuenta/{id}/{inicio}/{fin}")]
        public IEnumerable<string> GetEstadoCuenta(int id, DateTime inicio, DateTime fin)
        {
            return new string[] { "value1", "value2" };
        }

        
    }
}

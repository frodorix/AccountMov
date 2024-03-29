﻿using AccountMovAPI.DTO;
using CORE.Account.Application.Interfaces;
using CORE.Account.Domain.Model;
using CORE.Account.Exception;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountMovAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        public readonly IMovimientosService movimientosService;
        private readonly ILogger<MovimientosController> _logger;
        public MovimientosController(IMovimientosService movimientosService, ILogger<MovimientosController> _logger)
        {
            this.movimientosService = movimientosService;
            this._logger = _logger;
        }
        // GET: api/<MovimientosController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MovimientosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = await this.movimientosService.ObtenerPorId(id);
            if (cliente == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(cliente, new StringEnumConverter());
            return Ok(json);
        }

        // POST api/<MovimientosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DNuevoMovimiento json)
        {
            if (json == null) return BadRequest("JSon Invalido");
            try
            {

                var movimiento = await this.movimientosService.Crear(json.toMovimiento());
                var response = JsonConvert.SerializeObject(movimiento.Id, new StringEnumConverter());
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


        // PUT api/<MovimientosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DMovimientoModificable json)
        {
            if (json == null) return BadRequest();
            json.Id = id;
            try
            {
                _= await this.movimientosService.Modificar(json.toMovimiento());
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

        // DELETE api/<MovimientosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _ = await this.movimientosService.Eliminar(id);
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

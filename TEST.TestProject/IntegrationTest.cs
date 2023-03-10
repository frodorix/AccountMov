using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;

namespace TEST.TestProject
{
    public class IntegrationTest
    {
        private string server = "localhost:8432";

        public int ClienteId { get; private set; } = 1;
        
        public int NumeroCuenta { get; private set; } = 1;

        [SetUp]
        public void Setup() { }
        [Test]
        public async Task Test1_CrearClient()
        {
            try
            {
                HttpStatusCode estado;
                string content;
                using (var httpClient = new HttpClient())
                {
                    var cliente = new { Nombre = "Oscar Rojas", Genaro = EGenero.masculino, Edad = 14, Identificacion = DateTime.Now.ToString("ddMMHHmmss")+"SC", Direccion = "Avenida Siempre viva", Telefono = "7451245", Contrasena = "contraseña" };
                    var json = JsonConvert.SerializeObject(cliente, new StringEnumConverter());
                    Console.WriteLine("Enviando nuevo cliente:");
                    Console.WriteLine(json);
                    StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync($"http://{server}/api/Clientes/", stringContent);
                    content = await response.Content.ReadAsStringAsync();
                    estado = response.StatusCode;
                }
                if (estado== HttpStatusCode.Created)
                {
                    

                    this.ClienteId = Int32.Parse(content);
                    Console.WriteLine($"ClienteID: {content}");
                    return;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                
            }
            Assert.Fail();
        }
        [Test]
        public async Task Test2_GetCliente()
        {
            HttpStatusCode estado;
            string content;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"http://{server}/api/Clientes/{ClienteId}");
                content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                estado = response.StatusCode;
                if (estado == HttpStatusCode.OK)
                {                    
                    return;
                }
            }
           

            Assert.Fail();
        }

        [Test]
        public async Task Test3_CrearCuenta()
        {
            HttpStatusCode estado;
            string content;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var cuenta = new { ClienteId = this.ClienteId, SaldoInicial = 100, Tipo = ETipoCuenta.Caja_de_ahorros };
                    var json = JsonConvert.SerializeObject(cuenta, new StringEnumConverter());
                    Console.WriteLine("Enviando nueva cuenta");
                    Console.WriteLine(json);
                    StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync($"http://{server}/api/Cuentas/", stringContent);
                    content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    estado = response.StatusCode;
                }
                if (estado == HttpStatusCode.Created)
                {
                    this.NumeroCuenta = Int32.Parse(content);
                    Console.WriteLine($"nuevo NumeroCuenta: {content}");
                    return;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }
            Assert.Fail();
        }

        [Test]
        public async Task Test4_CrearMovimiento()
        {
            var moviento = new { NumeroCuenta = this.NumeroCuenta, Tipo = ETipoMovimiento.Credito, Valor = 100 };
            HttpStatusCode estado;
            string content;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(moviento, new StringEnumConverter());
                    Console.WriteLine("Enviando nuevo moviento");
                    Console.WriteLine(json);
                    StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync($"http://{server}/api/Movimientos/", stringContent);
                    content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    estado = response.StatusCode;
                }
                if (estado == HttpStatusCode.Created)
                {
                    this.NumeroCuenta = Int32.Parse(content);
                    Console.WriteLine($"nuevo Movimiento ID: {content}");
                    return;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }
            Assert.Fail();
        }



    }
}

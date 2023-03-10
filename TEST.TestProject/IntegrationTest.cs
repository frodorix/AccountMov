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

        private int id;
        public int ClienteId { get { return (id < 1)? 1:id; } private set { this.id = value; }
        }
        public int NumeroCuenta { get; private set; }

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
                    Console.WriteLine("Enviando nuevo cliente");
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
        public async Task Test4_CrearMovimiento()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var cuenta = new {NumeroCuenta=1, Tipo = ETipoMovimiento.Credito, Valor = 100 };
                    var json = JsonConvert.SerializeObject(cuenta, new StringEnumConverter());
                    Console.WriteLine("Enviando nuevo Movimiento");
                    Console.WriteLine(json);
                    StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync($"http://{server}/api/Movimientos/", stringContent);
                    var status = response.IsSuccessStatusCode;
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"nuevo ID MOVIMIENTO: {content}");
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {                 
                        return;
                    }
                    
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

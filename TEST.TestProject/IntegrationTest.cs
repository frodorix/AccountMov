using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST.TestProject
{
    public class IntegrationTest
    {
        private string server = "localhost:5133";
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public async Task GetCliente()
        {
            using (var httpClient = new HttpClient())
            {


                var response = await httpClient.GetAsync($"http://{server}/api/Clientes/1");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            Assert.Pass();
        }

    }
}

using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

using Backend_api.Models;
using Microsoft.AspNetCore.Hosting;
using TuNombreDeProyecto;

namespace MiProyecto.Pruebas
{
    [TestFixture]
    public class ControladorAPITests
    {
        private TestServer _server;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            _server = new TestServer(builder);
            _client = _server.CreateClient();
        }

        [Test]
        public async Task EmisoresEndpoint_ReturnsEmisores()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/ControladorAPI/api/v1/emisores");

            // Assert
            response.EnsureSuccessStatusCode(); // Verifica que el código de respuesta sea 200
            var emisores = await response.Content.ReadAsAsync<List<Emisor>>(); // Deserializa la respuesta en una lista de objetos Emisor
            Assert.That(emisores, Is.Not.Null); // Verifica que la lista no sea nula
            Assert.That(emisores, Is.Not.Empty); // Verifica que la lista no esté vacía
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _server.Dispose();
        }
    }
}

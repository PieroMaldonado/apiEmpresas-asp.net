using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using Backend_api.Models;
using Microsoft.AspNetCore.Hosting;
using TuNombreDeProyecto;
using System.Net;

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
        public async Task GetEmisoresAsync_ReturnsUnauthorized()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/ControladorAPI/api/v1/emisores");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized)); // Verifica que el código de estado sea 401 (No autorizado)
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _server.Dispose();
        }
    }
}

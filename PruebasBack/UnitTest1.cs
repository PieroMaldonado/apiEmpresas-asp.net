
using Backend_api.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http;

namespace Tests
{
    [TestFixture]
    public class MyApiTests
    {
        private HttpClient _client;
        private string _baseUrl = "https://localhost:7018/api/ControladorAPI/api/v1/";

        [SetUp]
        public void Setup()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }

        [Test]
        public async Task GetEmisores_ReturnsEmisores()
        {
            // Arrange
            var id = 1;
            var expectedEmisor = new Emisor { Id = 1, NombreEmisor = "Emisor 1" };
            var client = new HttpClient();
            var response = await client.GetAsync($"https://localhost:7018/api/ControladorAPI/api/v1/emisores/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var emisor = JsonConvert.DeserializeObject<Emisor>(content);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedEmisor.Id, emisor.Id);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedEmisor.NombreEmisor, emisor.NombreEmisor);
        }
    }


}

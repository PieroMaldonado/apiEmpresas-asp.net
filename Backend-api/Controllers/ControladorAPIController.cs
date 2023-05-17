using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using Backend_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Plugins;

namespace Backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorAPIController : ControllerBase
    {

        [HttpGet]
        [Authorize]
        [Route("api/v1/emisores")]
        public async Task<ActionResult<List<Emisor>>> GetEmisoresAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/GetEmisor");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return Ok(json);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        private readonly HttpClient _httpClient;

        public ControladorAPIController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("login")]
        [Authorize]
        public async Task<ActionResult> Login(LoginModel login)
        {
            _httpClient.BaseAddress = new Uri("http://apiservicios.ecuasolmovsa.com:3009");

            var response = await _httpClient.GetAsync($"/api/Usuarios?usuario={login.usuario}&password={login.contrasena}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/v1/centrocostos")]
        public async Task<ActionResult<List<CentroCostos>>> GetCentroCostosAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosSelect");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("CentroCostosInsert")]
        public async Task<ActionResult> AgregarCentroCostoAsync(int codigoCentroCostos, string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de codigoCentroCostos es: " + codigoCentroCostos);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosInsert?codigocentrocostos={codigoCentroCostos}&descripcioncentrocostos={descripcionCentroCostos}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/centrocostos/delete")]
        public async Task<ActionResult> DeleteCentroCostosAsync(int codigoCentroCostos, string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de codigoCentroCostos es: " + codigoCentroCostos);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosDelete?codigocentrocostos={codigoCentroCostos}&descripcioncentrocostos={descripcionCentroCostos}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/centrocostos/search")]
        public async Task<ActionResult> SearchCentroCostosAsync(string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de descripcion es: " + descripcionCentroCostos);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosSearch?descripcioncentrocostos={descripcionCentroCostos}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("CentroCostosEdit")]
        public async Task<ActionResult> EditarCentroCostoAsync(int codigoCentroCostos, string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de codigoCentroCostos es: " + codigoCentroCostos);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosUpdate?codigocentrocostos={codigoCentroCostos}&descripcioncentrocostos={descripcionCentroCostos}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/GetMovimientosPlanilla")]
        public async Task<ActionResult<string>> GetMovimientosPlanillaAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/MovimientoPlanillaSelect");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpGet("loginAutorizador")]
        [Authorize]
        public async Task<ActionResult> Login(string usuario, string password)
        {
            _httpClient.BaseAddress = new Uri("http://apiservicios.ecuasolmovsa.com:3009");

            var response = await _httpClient.GetAsync($"/api/Varios/GetAutorizador?usuario={usuario}&password={password}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("MovimientoPlanillaInsert")]
        public async Task<ActionResult> InsertarMovimientoPlanillaAsync(string conceptos, int prioridad, string tipooperacion, int cuenta1, int cuenta2, int cuenta3, int cuenta4, string MovimientoExcepcion1, string MovimientoExcepcion2, string MovimientoExcepcion3, int Traba_Aplica_iess, int Traba_Proyecto_imp_renta, int Aplica_Proy_Renta, int Empresa_Afecta_Iess)
        {
            Console.WriteLine("El valor de conceptos es: " + conceptos);

            var httpClient = new HttpClient();
            var url = $"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/MovimientoPlanillaInsert?conceptos={conceptos}&prioridad={prioridad}&tipooperacion={tipooperacion}&cuenta1={cuenta1}&cuenta2={cuenta2}&cuenta3={cuenta3}&cuenta4={cuenta4}&MovimientoExcepcion1={MovimientoExcepcion1}&MovimientoExcepcion2={MovimientoExcepcion2}&MovimientoExcepcion3={MovimientoExcepcion3}&Traba_Aplica_iess={Traba_Aplica_iess}&Traba_Proyecto_imp_renta={Traba_Proyecto_imp_renta}&Aplica_Proy_Renta={Aplica_Proy_Renta}&Empresa_Afecta_Iess={Empresa_Afecta_Iess}";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("ObtenerMovimientosExcepcion1y2")]
        public async Task<ActionResult> ObtenerMovimientosExcepcionAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/MovimientosExcepcion1y2";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("ObtenerMovimientosExcepcion3")]
        public async Task<ActionResult> ObtenerMovimientosExcepcion3Async()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/MovimientosExcepcion3";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetTipoOperacion")]
        public async Task<ActionResult> GetTipoOperacionAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoOperacion";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetTrabaAfectaIESS")]
        public async Task<ActionResult> GetTrabaAfectaIessAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabaAfectaIESS";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetTrabAfecImpuestoRenta")]
        public async Task<ActionResult> GetTrabAfecImpuestoRentaAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabAfecImpuestoRenta";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/movimientoPlanilla/delete")]
        public async Task<ActionResult> DeleteMovimientoPlanillaAsync(int codigomovimiento, string descripcionomovimiento)
        {
            Console.WriteLine("El valor de codigo es: " + codigomovimiento);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/MovimeintoPlanillaDelete?codigomovimiento={codigomovimiento}&descripcionomovimiento={descripcionomovimiento}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/movimientoPlanilla/edit")]
        public async Task<ActionResult> EditarMovimientoPlanillaAsync(int codigoplanilla, string conceptos, int prioridad, string tipooperacion, int cuenta1, int cuenta2, int cuenta3, int cuenta4, string MovimientoExcepcion1, string MovimientoExcepcion2, string MovimientoExcepcion3, int Traba_Aplica_iess, int Traba_Proyecto_imp_renta, int Aplica_Proy_Renta, int Empresa_Afecta_Iess)
        {
            Console.WriteLine("El valor de codigoMovimientoPlanilla es: " + codigoplanilla);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/MovimientoPlanillaUpdate?codigoplanilla={codigoplanilla}&conceptos={conceptos}&prioridad={prioridad}&tipooperacion={tipooperacion}&cuenta1={cuenta1}&cuenta2={cuenta2}&cuenta3={cuenta3}&cuenta4={cuenta4}&MovimientoExcepcion1={MovimientoExcepcion1}&MovimientoExcepcion2={MovimientoExcepcion2}&MovimientoExcepcion3={MovimientoExcepcion3}&Traba_Aplica_iess={Traba_Aplica_iess}&Traba_Proyecto_imp_renta={Traba_Proyecto_imp_renta}&Aplica_Proy_Renta={Aplica_Proy_Renta}&Empresa_Afecta_Iess={Empresa_Afecta_Iess}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/movimientoPlanilla/search")]
        public async Task<ActionResult> SearchMovimientoPlanillasAsync(string concepto)
        {
            Console.WriteLine("El valor de descripcion es: " + concepto);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/MovimientoPlanillaSearch?Concepto={concepto}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
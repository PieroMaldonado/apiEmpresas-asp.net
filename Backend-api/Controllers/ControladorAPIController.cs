using System.Web;
using Backend_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Kms.V1;
using Google.Protobuf;
using System.Text;

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
            var response = await httpClient.GetAsync($"{_apiUrl}/api/Varios/GetEmisor");

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
        private readonly string _apiUrl;

        public ControladorAPIController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiUrl = Environment.GetEnvironmentVariable("API_URL");
        }

        [HttpPost("login")]
        [Authorize]
        public async Task<ActionResult> Login(LoginModel login)
        {
            _httpClient.BaseAddress = new Uri(_apiUrl);

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
        [Route("api/v1/centrocostos")]
        [Authorize(Policy = "ReadAccess")]
        public async Task<ActionResult<List<CentroCostos>>> GetCentroCostosAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{_apiUrl}/api/Varios/CentroCostosSelect");

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
        [Route("CentroCostosInsert")]
        [Authorize(Policy = "WriteAccess")]
        public async Task<ActionResult> AgregarCentroCostoAsync(int codigoCentroCostos, string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de codigoCentroCostos es: " + codigoCentroCostos);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{_apiUrl}/api/Varios/CentroCostosInsert?codigocentrocostos={codigoCentroCostos}&descripcioncentrocostos={descripcionCentroCostos}");

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
        [Route("api/centrocostos/delete")]
        [Authorize(Policy = "DeleteAccess")]
        public async Task<ActionResult> DeleteCentroCostosAsync(int codigoCentroCostos, string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de codigoCentroCostos es: " + codigoCentroCostos);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{_apiUrl}/api/Varios/CentroCostosDelete?codigocentrocostos={codigoCentroCostos}&descripcioncentrocostos={descripcionCentroCostos}");

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
        [Route("api/centrocostos/search")]
        [Authorize(Policy = "ReadAccess")]
        public async Task<ActionResult> SearchCentroCostosAsync(string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de descripcion es: " + descripcionCentroCostos);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{_apiUrl}/api/Varios/CentroCostosSearch?descripcioncentrocostos={descripcionCentroCostos}");

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
        [Route("CentroCostosEdit")]
        [Authorize(Policy = "WriteAccess")]
        public async Task<ActionResult> EditarCentroCostoAsync(int codigoCentroCostos, string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de codigoCentroCostos es: " + codigoCentroCostos);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{_apiUrl}/api/Varios/CentroCostosUpdate?codigocentrocostos={codigoCentroCostos}&descripcioncentrocostos={descripcionCentroCostos}");

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
        [Route("api/GetMovimientosPlanilla")]
        [Authorize(Policy = "ReadAccess")]
        public async Task<ActionResult<string>> GetMovimientosPlanillaAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{_apiUrl}/api/Varios/MovimientoPlanillaSelect");

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
            _httpClient.BaseAddress = new Uri(_apiUrl);

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
        [Route("MovimientoPlanillaInsert")]
        [Authorize(Policy = "WriteAccess")]
        public async Task<ActionResult> InsertarMovimientoPlanillaAsync(string conceptos, int prioridad, string tipooperacion, int cuenta1, int cuenta2, int cuenta3, int cuenta4, string MovimientoExcepcion1, string MovimientoExcepcion2, string MovimientoExcepcion3, int Traba_Aplica_iess, int Traba_Proyecto_imp_renta, int Aplica_Proy_Renta, int Empresa_Afecta_Iess)
        {
            Console.WriteLine("El valor de conceptos es: " + conceptos);

            var httpClient = new HttpClient();
            var url = $"{_apiUrl}/api/Varios/MovimientoPlanillaInsert?conceptos={conceptos}&prioridad={prioridad}&tipooperacion={tipooperacion}&cuenta1={cuenta1}&cuenta2={cuenta2}&cuenta3={cuenta3}&cuenta4={cuenta4}&MovimientoExcepcion1={MovimientoExcepcion1}&MovimientoExcepcion2={MovimientoExcepcion2}&MovimientoExcepcion3={MovimientoExcepcion3}&Traba_Aplica_iess={Traba_Aplica_iess}&Traba_Proyecto_imp_renta={Traba_Proyecto_imp_renta}&Aplica_Proy_Renta={Aplica_Proy_Renta}&Empresa_Afecta_Iess={Empresa_Afecta_Iess}";

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
            var _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_apiUrl);
            var url = "/api/Varios/MovimientosExcepcion1y2";
            var response = await _httpClient.GetAsync(url);

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
            var _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_apiUrl);
            var url = "/api/Varios/MovimientosExcepcion3";
            var response = await _httpClient.GetAsync(url);

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
            httpClient.BaseAddress = new Uri(_apiUrl);
            var url = "/api/Varios/TipoOperacion";
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
            httpClient.BaseAddress = new Uri(_apiUrl);
            var url = "/api/Varios/TrabaAfectaIESS";
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
            httpClient.BaseAddress = new Uri(_apiUrl);
            var url = "/api/Varios/TrabAfecImpuestoRenta";
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
        [Route("api/movimientoPlanilla/delete")]
        [Authorize(Policy = "WriteAccess")]
        public async Task<ActionResult> DeleteMovimientoPlanillaAsync(int codigomovimiento, string descripcionomovimiento)
        {
            Console.WriteLine("El valor de codigo es: " + codigomovimiento);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{_apiUrl}/api/Varios/MovimeintoPlanillaDelete?codigomovimiento={codigomovimiento}&descripcionomovimiento={descripcionomovimiento}");

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
        [Route("api/movimientoPlanilla/edit")]
        [Authorize(Policy = "WriteAccess")]
        public async Task<ActionResult> EditarMovimientoPlanillaAsync(int codigoplanilla, string conceptos, int prioridad, string tipooperacion, int cuenta1, int cuenta2, int cuenta3, int cuenta4, string MovimientoExcepcion1, string MovimientoExcepcion2, string MovimientoExcepcion3, int Traba_Aplica_iess, int Traba_Proyecto_imp_renta, int Aplica_Proy_Renta, int Empresa_Afecta_Iess)
        {
            Console.WriteLine("El valor de codigoMovimientoPlanilla es: " + codigoplanilla);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{_apiUrl}/api/Varios/MovimientoPlanillaUpdate?codigoplanilla={codigoplanilla}&conceptos={conceptos}&prioridad={prioridad}&tipooperacion={tipooperacion}&cuenta1={cuenta1}&cuenta2={cuenta2}&cuenta3={cuenta3}&cuenta4={cuenta4}&MovimientoExcepcion1={MovimientoExcepcion1}&MovimientoExcepcion2={MovimientoExcepcion2}&MovimientoExcepcion3={MovimientoExcepcion3}&Traba_Aplica_iess={Traba_Aplica_iess}&Traba_Proyecto_imp_renta={Traba_Proyecto_imp_renta}&Aplica_Proy_Renta={Aplica_Proy_Renta}&Empresa_Afecta_Iess={Empresa_Afecta_Iess}");

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
        [Route("api/movimientoPlanilla/search")]
        [Authorize(Policy = "ReadAccess")]
        public async Task<ActionResult> SearchMovimientoPlanillasAsync(string concepto)
        {
            Console.WriteLine("El valor de descripcion es: " + concepto);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{_apiUrl}/api/Varios/MovimientoPlanillaSearch?Concepto={concepto}");

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
        [Route("validarCedula")]
        public async Task<ActionResult> validarCedula(string cedula)
        {
            //variables de entorno para el servicio de Google Cloud KMS
            string projectId = Environment.GetEnvironmentVariable("projectId");
            string locationId = Environment.GetEnvironmentVariable("locationId");
            string keyRingId = Environment.GetEnvironmentVariable("keyRingId");
            string keyId = Environment.GetEnvironmentVariable("keyId");

            //Configurar variable de entorno hacia la ruta del .json de las credenciales de Google
            string credential_path = Environment.GetEnvironmentVariable("credential_path");
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);

            // Create the client.
            KeyManagementServiceClient client = KeyManagementServiceClient.Create();

            // Build the key name.
            CryptoKeyName keyName = new CryptoKeyName(projectId, locationId, keyRingId, keyId);

            // Decodificar la cadena de texto en base64
            string decodedCedula = HttpUtility.UrlDecode(cedula);

            // Reemplazar los espacios en blanco por el símbolo "+"
            decodedCedula = decodedCedula.Replace(" ", "+");

            // Call the API.
            DecryptResponse result = client.Decrypt(keyName, ByteString.CopyFrom(Convert.FromBase64String(decodedCedula)));

            // Get the plaintext. Cryptographic plaintexts and ciphertexts are
            // always byte arrays.
            byte[] plaintext = result.Plaintext.ToByteArray();

            // Convertir el valor de plaintext a un arreglo de caracteres
            char[] numeroIdentificacion = Encoding.UTF8.GetChars(plaintext);

            return Ok(VerificaCedula(numeroIdentificacion));


        }

        private static bool VerificaCedula(char[] validarCedula)
        {
            int aux = 0, par = 0, impar = 0, verifi;
            for (int i = 0; i < 9; i += 2)
            {
                aux = 2 * int.Parse(validarCedula[i].ToString());
                if (aux > 9)
                    aux -= 9;
                par += aux;
            }
            for (int i = 1; i < 9; i += 2)
            {
                impar += int.Parse(validarCedula[i].ToString());
            }

            aux = par + impar;
            if (aux % 10 != 0)
            {
                verifi = 10 - (aux % 10);
            }
            else
                verifi = 0;
            if (verifi == int.Parse(validarCedula[9].ToString()))
                return true;
            else
                return false;
        }
    }
}
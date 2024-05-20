using RegistroActividades.Models.DTOS;
using RegistroDeActividades.Models.DTOS;
using System.Net.Http;
using System.Net.Http.Json;

namespace RegistroActividades.Servicies
{
    public class DepartamentosService
    {

        private readonly string url = "https://registro-actividades-equipo-dos.websitos256.com/api/";

        private readonly HttpClient _client;

        public DepartamentosService()
        {

            var token = UserSettings.Default.Token;
            _client = new HttpClient()
            {
                BaseAddress = new Uri(url),
                DefaultRequestHeaders = { Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token) }
            };
        }

        public async Task<List<DepartamentoDTO>> Get()
        {
            var response = await _client.GetAsync("departamento");


            if (response.IsSuccessStatusCode)
            {
                var departamentos = await response.Content.ReadFromJsonAsync<List<DepartamentoDTO>>();

                return departamentos;
            }
            else
                return null;

        }

        public async Task<DepartamentoDTO> Get(int id)
        {
            var response = await _client.GetAsync($"departamento/{id}");


            if (response.IsSuccessStatusCode)
            {
                var departamento = await response.Content.ReadFromJsonAsync<DepartamentoDTO>();

                return departamento;
            }
            else
                return null;

        }


        public async Task Post(DepartamentoCreateDTO departamento)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserSettings.Default.Token);
                 await _client.PostAsJsonAsync("departamento", departamento);

            }
            catch (Exception)
            {
                    Console.WriteLine("Error al guardar el departamento");
            }
        }

        public async Task Put(DepartamentoCreateDTO departamento)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserSettings.Default.Token);
                var response = await _client.PutAsJsonAsync("departamento", departamento);

            }
            catch (Exception)
            {
                Console.WriteLine("Error al actualizar el departamento");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserSettings.Default.Token);
                var response = await _client.DeleteAsync($"departamento/{id}");

            }
            catch (Exception)
            {
                Console.WriteLine("Error al eliminar el departamento");
            }
        }
    }
}

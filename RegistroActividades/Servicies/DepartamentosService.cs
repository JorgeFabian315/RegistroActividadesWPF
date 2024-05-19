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
            _client = new HttpClient() { BaseAddress = new Uri(url) };
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


        public async Task<DepartamentoDTO> Post(DepartamentoCreateDTO departamento)
        {
            var response = await _client.PostAsJsonAsync("departamento", departamento);

            if (response.IsSuccessStatusCode)
            {
                var departamentoCreado = await response.Content.ReadFromJsonAsync<DepartamentoDTO>();

                return departamentoCreado;
            }
            else
                return null;

        }
    }
}

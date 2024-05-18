using RegistroDeActividades.Models.DTOS;
using System.Net.Http;
using System.Net.Http.Json;

namespace RegistroActividades.Servicies
{
    public class ActividadesService
    {

        private readonly string url = "https://registro-actividades-equipo-dos.websitos256.com/api/";
        private readonly HttpClient _client;

        public ActividadesService()
        {
            _client = new HttpClient() { BaseAddress = new Uri(url) };
        }


        public async Task<string?> IniciarSesion(LoginDTO user)
        {
            var response = await _client.PostAsJsonAsync("login", user);

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();

                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                return token;
            }
            else
                return null;

        }










    }
}

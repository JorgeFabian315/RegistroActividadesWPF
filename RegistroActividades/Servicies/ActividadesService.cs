using RegistroActividades.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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


        public async Task<string?> IniciarSesion(Usuario user)
        {
            var response = await _client.PostAsJsonAsync("login", user);

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var token =  await response.Content.ReadAsStringAsync();

                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                return token;
            }
            else
                return null;

        }










    }
}

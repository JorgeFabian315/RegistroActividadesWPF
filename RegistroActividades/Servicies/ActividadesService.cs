using RegistroActividades.Repositories;
using RegistroDeActividades.Enums;
using RegistroDeActividades.Models.DTOS;
using RegistroDeActividades.Models.Entities;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows;

namespace RegistroActividades.Servicies
{
    public class ActividadesService
    {

        private readonly string url = "https://registro-actividades-equipo-dos.websitos256.com/api/";
        private readonly HttpClient _client;
        private string? _token;
        private ActividadesRepository _actividadesRepository = new();
        public event Action? DatosActualizados;
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

                _token = token;

                RegistroActividades.UserSettings.Default.Token = token;
                UserSettings.Default.Save();

                return token;
            }
            else
                return null;

        }

        public async Task Get() 
        {
            try
            {
                var fecha = RegistroActividades.UserSettings.Default.UltimaFecha;

                bool aviso = false;
                var response = await _client.GetFromJsonAsync<List<ActividadDTO>>($"actividad/{fecha:yyyy-MM-dd}/{fecha:HH}/{fecha:mm}");
                if(response != null)
                {
                    foreach (var actividad in response)
                    {
                        var entidad = _actividadesRepository.Get(actividad.Id ?? 0);

                        if(entidad == null && entidad?.Estado != (int)Estados.Eliminada)
                        {
                            entidad = new Actividades()
                            {
                                Id = actividad.Id ?? 0,
                                Titulo = actividad.Titulo,
                                Descripcion = actividad.Descripcion,
                                FechaActualizacion = actividad.FechaActualizacion ?? DateTime.UtcNow,
                                FechaCreacion = actividad.FechaCreacion ?? DateTime.UtcNow,
                                FechaRealizacion = actividad.FechaRealizacion ?? DateTime.UtcNow,
                                IdDepartamento = actividad.DepartamentoId,
                                Estado = actividad.Estado,
                            };

                            _actividadesRepository.Insert(entidad);
                            aviso = true;
                        }
                        else
                        {
                            if(entidad.Estado == (int)Estados.Eliminada)
                            {
                                _actividadesRepository.Delete(entidad);
                                aviso = true;
                            }
                            else
                            {
                                if(!actividad.Equals(entidad))
                                {
                                    _actividadesRepository.Update(entidad);
                                    aviso = true;
                                }
                            }
                        }


                    }
                    if (aviso)
                    {

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            DatosActualizados?.Invoke();
                        });
                    }
                    var fechaMaxima = response.Max(x => x.FechaActualizacion) ?? DateTime.UtcNow;  

                    RegistroActividades.UserSettings.Default.UltimaFecha = fechaMaxima;
                }





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }









    }
}

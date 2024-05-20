using RegistroActividades.Repositories;
using RegistroDeActividades.Enums;
using RegistroDeActividades.Models.DTOS;
using RegistroDeActividades.Models.Entities;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;

namespace RegistroActividades.Servicies
{
    public class ActividadesService
    {

        private readonly string url = "https://registro-actividades-equipo-dos.websitos256.com/api/";
        //private readonly string url = "https://localhost:7051/api/";
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
            try
            {
                var response = await _client.PostAsJsonAsync("login", user);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();

                    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    _token = token;

                    UserSettings.Default.Token = token;
                    UserSettings.Default.Save();

                    return token;
                }
                else
                    return null;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return null;
            }   

        }

        public async Task Get()
        {
            try
            {
                var fecha = UserSettings.Default.UltimaFecha;

                bool aviso = false;
                var response = await _client.GetFromJsonAsync<List<ActividadDTO>>($"actividad/{fecha:yyyy-MM-dd}/{fecha:HH}/{fecha:mm}");

                if (response != null && response.Any())
                {
                    foreach (var actividad in response)
                    {

                          var  entidad = _actividadesRepository.Get(actividad.Id ?? 0);
                        
                        
                        if (entidad == null && actividad?.Estado != (int)Estados.Eliminada)
                        {
                            entidad = new Actividades()
                            {
                                Id = actividad.Id ?? 0,
                                Titulo = actividad.Titulo,
                                Descripcion = actividad.Descripcion,
                                FechaActualizacion = actividad.FechaActualizacion ?? DateTime.UtcNow,
                                FechaCreacion = actividad.FechaCreacion ?? DateTime.UtcNow,
                                FechaRealizacion = actividad.FechaRealizacion,
                                IdDepartamento = actividad.DepartamentoId,
                                Estado = actividad.Estado,
                                NombreDepartamento = actividad.Departamento ?? string.Empty,
                                Imagen = actividad.Imagen ?? string.Empty
                            };

                            _actividadesRepository.Insert(entidad);
                            aviso = true;
                        }
                        else
                        {
                            entidad = new Actividades()
                            {
                                Id = actividad.Id ?? 0,
                                Titulo = actividad.Titulo,
                                Descripcion = actividad.Descripcion,
                                FechaActualizacion = actividad.FechaActualizacion ?? DateTime.UtcNow,
                                FechaCreacion = actividad.FechaCreacion ?? DateTime.UtcNow,
                                FechaRealizacion = actividad.FechaRealizacion,
                                IdDepartamento = actividad.DepartamentoId,
                                Estado = actividad.Estado,
                                NombreDepartamento = actividad.Departamento ?? string.Empty,
                                Imagen = actividad.Imagen ?? string.Empty
                            };

                            if (actividad.Estado == (int)Estados.Eliminada)
                            {
                               var eliminado = _actividadesRepository.Delete(entidad);
                                aviso = true;
                            }
                            else
                            {
                                if (!actividad.Equals(entidad))
                                {

                                    var r = _actividadesRepository.Update(entidad);
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
                    var fechaMaxima = response.Max(x => x.FechaActualizacion) ?? DateTime.MinValue;

                    UserSettings.Default.UltimaFecha = fechaMaxima;
                    UserSettings.Default.Save();
                }





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }




        public async Task Post(ActividadDTO actividad)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserSettings.Default.Token);
                var response = await _client.PostAsJsonAsync("actividad", actividad);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                    await Get();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        public async Task Delete(int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserSettings.Default.Token);
                var response = await _client.DeleteAsync($"actividad/{id}");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                    await Get();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        public async Task Put(ActividadDTO actividad)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserSettings.Default.Token);
                var response = await _client.PutAsJsonAsync("actividad", actividad);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                    await Get();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        public async Task PostBorrador(ActividadDTO actividad)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserSettings.Default.Token);
                var response = await _client.PostAsJsonAsync("actividad/borrador", actividad);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                    await Get();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        public async Task PutBorrador(ActividadDTO actividad)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserSettings.Default.Token);
                var response = await _client.PutAsJsonAsync("actividad/borrador", actividad);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                    await Get();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }



    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroActividades.Models.Entities;
using RegistroActividades.Servicies;
using RegistroActividades.Views;
using RegistroDeActividades.Models.DTOS;

namespace RegistroActividades.ViewModels
{
    public partial class ActividadesViewModel : ObservableObject
    {

        [ObservableProperty]
        private Usuario usuario;

        [ObservableProperty]
        private VistaInicio vistaInicio;

        [ObservableProperty]
        private VistaActividades vistaActividad;

        [ObservableProperty]
        private ActividadDTO actividad;

        private readonly ActividadesService _actividadesService = new();

        [ObservableProperty]
        private string loginError;
        public ActividadesViewModel()
        {
            VistaActividad = VistaActividades.Listado;
            LoginError = string.Empty;
            Usuario = new Usuario();
            Actividad = new ActividadDTO();
        }




        [RelayCommand]
        public async Task IniciarSesion()
        {
            try
            {
                if (string.IsNullOrEmpty(Usuario.Username) || string.IsNullOrEmpty(Usuario.Password))
                    LoginError = "El campo usuario y contraseña no pueden estar vacíos.";
                else
                {
                    var token = await _actividadesService.IniciarSesion(Usuario);

                    if (token != null)
                    {
                        VistaInicio = VistaInicio.Home;
                    }
                    else
                        LoginError = "Error en el usuario o contraseña.";
                }
            }
            catch (Exception ex)
            {
                LoginError = ex.Message.ToString();
            }


        }

        [RelayCommand]
        public void VistaAgregarActividad()
        {
            VistaActividad = VistaActividades.Agregar;
        }

        [RelayCommand]
        public void Cancelar()
        {
            VistaActividad = VistaActividades.Listado;
        }


    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RegistroActividades.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        [ObservableProperty]
        private string username;
        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string loginError;

        public LoginViewModel()
        {
        }

        [RelayCommand]
        public void Login()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                LoginError = "El campo usuario y contraseña no pueden estar vacíos.";
            else
            {

                // LLamar al servicio de login
                //var token = await _actividadesService.IniciarSesion(Usuario);

                //if (token != null)
                //{
                //    VistaInicio = VistaInicio.Home;
                //}
            }
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroActividades.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        [ObservableProperty]
        private object currentViewModel;


        [ObservableProperty]
        private string username;
        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string loginError;

        [RelayCommand]
        public void Login ()
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

        public void ChangeViewModel (object viewModel)
        {
            currentViewModel = viewModel;
        }



    }
}

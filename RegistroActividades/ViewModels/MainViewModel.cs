using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroActividades.Servicies;
using RegistroActividades.Views;
using RegistroActividades.Views.LoginViews;
using RegistroDeActividades.Models.DTOS;
using RegistroDeActividades.Models.Validator;

namespace RegistroActividades.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        [ObservableProperty]
        private string username = "";
        [ObservableProperty]
        private string password = "";

        [ObservableProperty]
        private string loginError = "";

        [ObservableProperty]
        private object? currentViewModel;

        [ObservableProperty]
        private bool usuarioConectado = false;  

        private ActividadesService service = new();

        private ActividadesViewModel actividadesViewModel = new();

        private DepartamentosViewModel departamentosViewModel = new();
        public MainViewModel()
        {
            UsuarioConectado = false;
        }

        [RelayCommand]
        public async Task IniciarSesion()
        {
            var user = new LoginDTO() { Username = Username, Password = Password };
            var loginDTOValidator = new LoginDTOValidator();
            var result = loginDTOValidator.Validate(user);

            if (!result.IsValid)
                LoginError = result.Errors.Select(e => e.ErrorMessage).Aggregate((a, b) => a + Environment.NewLine + b);

            else
            {
                var token = await service.IniciarSesion(user);

                if (!string.IsNullOrWhiteSpace(token))
                {
                    UsuarioConectado = true;
                    CurrentViewModel = actividadesViewModel;
                }

            }

        }

        [RelayCommand]
        public void CambiarActividadesViewModel()
        {
            CurrentViewModel = actividadesViewModel;
        }

        [RelayCommand]
        public void CambiarDepartamentoViewModel()
        {
            CurrentViewModel = departamentosViewModel;
        }

    }
}

using CommunityToolkit.Mvvm.ComponentModel;

namespace RegistroActividades.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {


        public MainViewModel()
        {
            currentViewModel = new LoginViewModel();
        }

        [ObservableProperty]
        private object currentViewModel;

        public void ChangeViewModel(object viewModel)
        {
            CurrentViewModel = viewModel;
        }



    }
}

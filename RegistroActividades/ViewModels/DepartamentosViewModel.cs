using CommunityToolkit.Mvvm.ComponentModel;
using RegistroActividades.Views;

namespace RegistroActividades.ViewModels
{
    public partial class DepartamentosViewModel:ObservableObject
    {

        [ObservableProperty]
        private VistaDepartamentos vistaDepartamento;

        public DepartamentosViewModel()
        {
            VistaDepartamento = VistaDepartamentos.Listado;
        }


    }
}

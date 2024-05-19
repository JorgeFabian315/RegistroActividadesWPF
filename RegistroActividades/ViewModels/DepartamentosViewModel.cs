using CommunityToolkit.Mvvm.ComponentModel;
using RegistroActividades.Servicies;
using RegistroActividades.Views;
using RegistroDeActividades.Models.DTOS;

namespace RegistroActividades.ViewModels
{
    public partial class DepartamentosViewModel:ObservableObject
    {

        DepartamentosService departamentosService = new DepartamentosService();

        [ObservableProperty]
        private List<DepartamentoDTO> departamentos;


        [ObservableProperty]
        private VistaDepartamentos vistaDepartamento;

        public DepartamentosViewModel()
        {
            VistaDepartamento = VistaDepartamentos.Listado;
            GetDepartamentos();
        }

        public async void GetDepartamentos()
        {
            Departamentos = await departamentosService.Get();
        }

    }
}

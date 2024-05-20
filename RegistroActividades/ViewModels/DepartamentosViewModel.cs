using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroActividades.Servicies;
using RegistroActividades.Views;
using RegistroDeActividades.Models.DTOS;

namespace RegistroActividades.ViewModels
{
    public partial class DepartamentosViewModel:ObservableObject
    {

        DepartamentosService departamentosService = new DepartamentosService();

        [ObservableProperty]
        private DepartamentoCreateDTO departamento = new();

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

        [RelayCommand]
        public void VerAgregarDepartamento()
        {
            VistaDepartamento = VistaDepartamentos.Agregar;
        }

        [RelayCommand]
        public void VerListadoDepartamento()
        {
            VistaDepartamento = VistaDepartamentos.Listado;
        }

        [RelayCommand]
        public async void GuardarDepartamento()
        {

            await departamentosService.Post(Departamento);
            GetDepartamentos();
            VistaDepartamento = VistaDepartamentos.Listado;
        }
    }
}

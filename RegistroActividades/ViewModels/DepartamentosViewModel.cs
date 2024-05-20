using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroActividades.Servicies;
using RegistroActividades.Views;
using RegistroDeActividades.Models.DTOS;
using RegistroActividades.Models.DTOS;

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
            MainViewModel.VerListadDepartamentos += MainViewModel_VerListadDepartamentos; ;
            
            GetDepartamentos();
        }

        private void MainViewModel_VerListadDepartamentos()
        {
             VistaDepartamento = VistaDepartamentos.Listado;
        }

        public async void GetDepartamentos()
        {
            Departamentos = await departamentosService.Get();
        }

        [RelayCommand]
        public void VerAgregarDepartamento()
        {
            Departamento = new();
            VistaDepartamento = VistaDepartamentos.Agregar;
        }

        [RelayCommand]
        public void VerListadoDepartamento()
        {
            VistaDepartamento = VistaDepartamentos.Listado;
        }

        [RelayCommand]
        public void VerEditarDepartamento(DepartamentoDTO departamento)
        {
            Departamento = new DepartamentoCreateDTO
            {
                Id = departamento.Id,
                Nombre = departamento.Nombre,
                Username = departamento.Username,
                IdSuperior = (int)departamento.IdSuperior
                
                
            };
            VistaDepartamento = VistaDepartamentos.Editar;
        }


        [RelayCommand]
        public void VerEliminarDepartamento(DepartamentoDTO departamento)
        {
            Departamento = new DepartamentoCreateDTO
            {
                Id = departamento.Id,
                Nombre = departamento.Nombre,
                Username = departamento.Username,
                IdSuperior = (int)departamento.IdSuperior
            };
            VistaDepartamento = VistaDepartamentos.Eliminar;
        }

        [RelayCommand]
        public async void EliminarDepartamento()
        {
            await departamentosService.Delete(Departamento.Id);
            GetDepartamentos();
            VistaDepartamento = VistaDepartamentos.Listado;
        }

        [RelayCommand]
        public async void EditarDepartamento()
        {
            await departamentosService.Put(Departamento);
            GetDepartamentos();
            VistaDepartamento = VistaDepartamentos.Listado;
        }

        [RelayCommand]
        public void Cancelar ()
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

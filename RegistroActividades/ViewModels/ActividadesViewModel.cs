using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroActividades.Repositories;
using RegistroActividades.Servicies;
using RegistroActividades.Views;
using RegistroDeActividades.Models.DTOS;
using RegistroDeActividades.Models.Entities;
using RegistroDeActividades.Models.Validator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RegistroActividades.ViewModels
{
    public partial class ActividadesViewModel : ObservableObject
    {

        public ObservableCollection<Actividades> Actividades { get; set; } = new();
        [ObservableProperty]
        private VistaInicio vistaInicio;

        [ObservableProperty]
        private VistaActividades vistaActividad;

        [ObservableProperty]
        private ActividadDTO actividad;

        private readonly ActividadesService _actividadesService = new();

        [ObservableProperty]
        private string error;


        [ObservableProperty]
        private int idUsuario;

        private ActividadesRepository _actividadRepositorio = new();
        private DepartamentosRepository _departamentosRepositorio = new();

        public ActividadesViewModel()
        {
            VistaActividad = VistaActividades.Listado;
            Actividad = new ActividadDTO();
            Error = "";
            IdUsuario = App.IdUsuario;
            MainViewModel.VerPerfilView += App_VerPerfilView;
            MainViewModel.VerListadpActividades += MainViewModel_VerListadpActividades;
            ActualizarActividades();
            App.service.DatosActualizados += Service_DatosActualizados;

        }

        private void Service_DatosActualizados()
        {
            ActualizarActividades();
        }

        private void MainViewModel_VerListadpActividades()
        {
            VistaActividad = VistaActividades.Listado;

        }

        private void App_VerPerfilView()
        {
            VistaActividad = VistaActividades.VerMisActividades;
        }

        [RelayCommand]
        public void VistaAgregarActividad()
        {
            Actividad = new ActividadDTO();
            VistaActividad = VistaActividades.Agregar;
        }

        [RelayCommand]
        public void Cancelar()
        {
            VistaActividad = VistaActividades.Listado;
        }


        [RelayCommand]
        public async Task AgregarActividad()
        {
            ActividadDTOValidator validator = new(_departamentosRepositorio.GetAll());
            var results = validator.Validate(Actividad);

            if (!results.IsValid)
            {
                Error = results.Errors.Select(x => x.ErrorMessage).Aggregate((a, b) => a + Environment.NewLine + b);
            }
            else
            {
                Actividad.Estado = 1;

                var imagencodificada = System.IO.File.ReadAllBytes(Actividad.Imagen ?? "");
                Actividad.Imagen = Convert.ToBase64String(imagencodificada); // CODIFICAMOS LA FOTO

                Actividad.FechaRealizacion = DateOnly.FromDateTime(Actividad.FechaRealizacion2.Value);

                await App.service.Post(Actividad);
                ActualizarActividades();
                Cancelar();
            }
        }
        public void ActualizarActividades()
        {
            Actividades.Clear();
            var actividadesBD = _actividadRepositorio.GetAll().OrderBy(x => x.FechaActualizacion);

            foreach (var a in actividadesBD)
            {
                Actividades.Add(a);
            }
        }



    }
}

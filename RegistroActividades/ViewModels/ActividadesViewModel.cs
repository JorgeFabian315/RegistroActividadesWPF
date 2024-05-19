using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroActividades.Repositories;
using RegistroActividades.Servicies;
using RegistroActividades.Views;
using RegistroDeActividades.Models.DTOS;
using RegistroDeActividades.Models.Entities;
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
        private string loginError;

        private ActividadesRepository _repository = new();

        public ActividadesViewModel()
        {
            VistaActividad = VistaActividades.Listado;
            LoginError = string.Empty;
            Actividad = new ActividadDTO();
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
            VistaActividad = VistaActividades.Agregar;
        }

        [RelayCommand]
        public void Cancelar()
        {
            VistaActividad = VistaActividades.Listado;
        }

        public void ActualizarActividades()
        {
            Actividades.Clear();
            var actividadesBD = _repository.GetAll().OrderBy(x => x.FechaActualizacion);

            foreach (var a in actividadesBD)
            {
                Actividades.Add(a);
            }
        }



    }
}

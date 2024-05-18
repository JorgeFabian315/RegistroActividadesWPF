using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroActividades.Servicies;
using RegistroActividades.Views;
using RegistroDeActividades.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RegistroActividades.ViewModels
{
    public partial class ActividadesViewModel : ObservableObject
    {


        [ObservableProperty]
        private VistaInicio vistaInicio;

        [ObservableProperty]
        private VistaActividades vistaActividad;

        [ObservableProperty]
        private ActividadDTO actividad;

        private readonly ActividadesService _actividadesService = new();

        [ObservableProperty]
        private string loginError;
        public ActividadesViewModel()
        {
            VistaActividad = VistaActividades.Listado;
            LoginError = string.Empty;
            Actividad = new ActividadDTO();
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


    }
}

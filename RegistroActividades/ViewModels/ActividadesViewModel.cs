using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using RegistroActividades.Repositories;
using RegistroActividades.Servicies;
using RegistroActividades.Views;
using RegistroDeActividades.Enums;
using RegistroDeActividades.Models.DTOS;
using RegistroDeActividades.Models.Entities;
using RegistroDeActividades.Models.Validator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RegistroActividades.ViewModels
{
    public partial class ActividadesViewModel : ObservableObject
    {

        public ObservableCollection<Actividades> Actividades { get; set; } = new();
        public ObservableCollection<Actividades> MisActividades { get; set; } = new();

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

        [RelayCommand]
        private void VerPerfil()
        {
                IdUsuario = App.IdUsuario;
                VerMisActividades();
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

                var imagencodificada = System.IO.File.ReadAllBytes(Actividad.ImagenDecodificada ?? "");
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
            var actividadesBD = _actividadRepositorio.GetAll()
                .Where(x => x.Estado == (int)Estados.Activa)
                .OrderBy(x => x.FechaActualizacion);

            foreach (var a in actividadesBD)
            {
                Actividades.Add(a);
            }
        }

        public void VerMisActividades()
        {
            MisActividades.Clear();
            var actividadesBD = _actividadRepositorio
                .GetAll()
                .Where(x => x.IdDepartamento == App.IdUsuario && x.Estado != (int)Estados.Eliminada)
                .OrderBy(x => x.FechaActualizacion)
                .ToList();

            foreach (var a in actividadesBD)
            {
                MisActividades.Add(a);
            }
        }


        [RelayCommand]
        public void VerEliminar(Actividades a)
        {
            Actividad = new ActividadDTO()
            {
                Id = a.Id,
                Departamento = a.NombreDepartamento,
                Descripcion = a.Descripcion,
                FechaActualizacion = a.FechaActualizacion,
                Estado = a.Estado,
                DepartamentoId = a.IdDepartamento,
                FechaCreacion = a.FechaCreacion,
                Imagen = a.Imagen,
                FechaRealizacion2 = a.FechaRealizacion,
                Titulo = a.Titulo,
            };


            VistaActividad = VistaActividades.EliminarActividad;
        }


        [RelayCommand]
        public async Task EliminarActividad()
        {
            if (Actividad != null)
            {
                await App.service.Delete(Actividad.Id ?? 0);

                //var entidad = new Actividades()
                //{
                //    Id = Actividad.Id ?? 0,
                //    Titulo = Actividad.Titulo,
                //    Descripcion = Actividad.Descripcion,
                //    FechaActualizacion = Actividad.FechaActualizacion ?? DateTime.UtcNow,
                //    FechaCreacion = Actividad.FechaCreacion ?? DateTime.UtcNow,
                //    FechaRealizacion = Actividad.FechaRealizacion.Value.ToDateTime(new TimeOnly(0, 0)),
                //    IdDepartamento = Actividad.DepartamentoId,
                //    Estado = Actividad.Estado,
                //    NombreDepartamento = Actividad.Departamento ?? string.Empty,
                //    Imagen = Actividad.Imagen ?? string.Empty
                //};
                //_actividadRepositorio.Update(entidad);
                ActualizarActividades();
                Cancelar();
            }
        }

        [RelayCommand]
        public void VerEditar(Actividades a)
        {
            Actividad = new ActividadDTO()
            {
                Id = a.Id,
                Departamento = a.NombreDepartamento,
                Descripcion = a.Descripcion,
                FechaActualizacion = a.FechaActualizacion,
                Estado = a.Estado,
                DepartamentoId = a.IdDepartamento,
                FechaCreacion = a.FechaCreacion,
                Imagen = a.Imagen,
                FechaRealizacion2 =a.FechaRealizacion,
                Titulo = a.Titulo,
            };

            VistaActividad = VistaActividades.EditarActividad;
        }

        [RelayCommand]
        public async Task EditarActividaad()
        {
            ActividadDTOValidator validator = new(_departamentosRepositorio.GetAll());
            var results = validator.Validate(Actividad);

            if (!results.IsValid)
                Error = results.Errors.Select(x => x.ErrorMessage).Aggregate((a, b) => a + Environment.NewLine + b);
            
            else
            {
                Actividad.Estado = 1;

                if (!string.IsNullOrWhiteSpace(Actividad.ImagenDecodificada))
                {
                    var imagencodificada = System.IO.File.ReadAllBytes(Actividad.ImagenDecodificada);
                    Actividad.Imagen = Convert.ToBase64String(imagencodificada); // CODIFICAMOS LA FOTO
                }

                Actividad.FechaRealizacion = DateOnly.FromDateTime(Actividad.FechaRealizacion2.Value);

                await App.service.Put(Actividad);
                ActualizarActividades();
                Cancelar();
            }
        }



    }
}

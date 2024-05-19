﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RegistroActividades.Models.Entities;
using RegistroActividades.Servicies;
using RegistroActividades.Views;
using RegistroActividades.Views.LoginViews;
using RegistroDeActividades.Models.DTOS;
using RegistroDeActividades.Models.Validator;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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


        private ActividadesViewModel actividadesViewModel = new();

        private DepartamentosViewModel departamentosViewModel = new();

        public static event Action? VerPerfilView;
        public static event Action? VerListadpActividades;

        [ObservableProperty]
        private Usuario? usuario;

        public MainViewModel()
        {
            Username = "DirectorGeneralEquipo2@gmail.com";
            Password = "12345678";
            UsuarioConectado = false;
        }

        [RelayCommand]
        public async Task IniciarSesion()
        {
            try
            {
                var user = new LoginDTO() { Username = Username, Password = Password };
                var loginDTOValidator = new LoginDTOValidator();
                var result = loginDTOValidator.Validate(user);

                if (!result.IsValid)
                    LoginError = result.Errors.Select(e => e.ErrorMessage).Aggregate((a, b) => a + Environment.NewLine + b);

                else
                {
                    var token = await App.service.IniciarSesion(user);

                    if (!string.IsNullOrWhiteSpace(token))
                    {

                        var handler = new JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                        if (jsonToken == null)
                            throw new Exception("Token no válido.");

                        var userIdClaim = jsonToken.Claims.First(claim => claim.Type == "Id").Value;
                        var usernameClaim = jsonToken.Claims.First(claim => ClaimTypes.Name == claim.Type).Value;
                        var roleClaim = jsonToken.Claims.First(claim => ClaimTypes.Role == claim.Type).Value;

                        Usuario = new()
                        {
                            Id = int.Parse(userIdClaim),
                            UserName = usernameClaim,
                            Rol = roleClaim
                        };
                        
                        UsuarioConectado = true;
                        Sincronizador();
                        CurrentViewModel = actividadesViewModel;
                    }

                }
            }
            catch(Exception ex)
            {
                LoginError = "Usuario no encontrado.";
            }

        }

        [RelayCommand]
        public void CambiarActividadesViewModel()
        {
            CurrentViewModel = actividadesViewModel;
            VerListadpActividades?.Invoke();
        }

        [RelayCommand]
        public void CambiarDepartamentoViewModel()
        {
            CurrentViewModel = departamentosViewModel;
        }

        [RelayCommand]
        public void VerMisActividades()
        {
            VerPerfilView?.Invoke();
        }







        async void Sincronizador()
        {
            while (true)
            {
                await App.service.Get(); // _= Descartar la tarea 
                Thread.Sleep(TimeSpan.FromMinutes(1));
            }
        }











    }
}

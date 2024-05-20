using RegistroActividades.Servicies;
using RegistroActividades.ViewModels;
using System.Windows;

namespace RegistroActividades
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application

    {
        public static ActividadesService service = new();

        public static int IdUsuario = 0;
         public App()
        {
            MainViewModel.LlamarSincronizador += MainViewModel_LlamarSincronizador;
        }

        private void MainViewModel_LlamarSincronizador()
        {
            Thread hilo = new Thread(Sincronizador) { IsBackground = true };
            hilo.Start();
        }

        async void Sincronizador()
        {
            while (true)
            {
                await App.service.Get(); // _= Descartar la tarea 
                Thread.Sleep(TimeSpan.FromMinutes(2));
            }
        }



    }
}

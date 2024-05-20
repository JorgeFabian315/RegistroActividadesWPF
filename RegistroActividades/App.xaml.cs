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
        }

  



    }
}

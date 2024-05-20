using RegistroActividades.Servicies;
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

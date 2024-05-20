using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RegistroActividades.Views.Actividades
{
    /// <summary>
    /// Lógica de interacción para AgregarActividadView.xaml
    /// </summary>
    public partial class AgregarActividadView : UserControl
    {
        public AgregarActividadView()
        {
            InitializeComponent();
        }

        private void btnAgregarImagen_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files |*.jpg;";

            if (openFileDialog.ShowDialog() == true)
            {
                txtImagen.Text = openFileDialog.FileName;
                imgActividad.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            imgActividad.Source = null;
        }
    }
}

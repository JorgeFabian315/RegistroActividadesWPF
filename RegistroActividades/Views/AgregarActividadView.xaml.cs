using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegistroActividades.Views
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
                imgActividad.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }



        }
    }
}

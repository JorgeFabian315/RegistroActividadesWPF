using System.Windows.Controls;

namespace RegistroActividades.Views.Actividades
{
    /// <summary>
    /// Lógica de interacción para ListaActividadesView.xaml
    /// </summary>
    public partial class ListaActividadesView : UserControl
    {
        public ListaActividadesView()
        {
            InitializeComponent();
        }

        public static event Action<string>? FiltrarActividadesEvent;
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            var selectedValue = comboBox.SelectedValue ?? "";
            FiltrarActividadesEvent?.Invoke(selectedValue.ToString());
        }
    }
}

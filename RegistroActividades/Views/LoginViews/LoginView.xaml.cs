using System.Windows;
using System.Windows.Controls;

namespace RegistroActividades.Views.LoginViews
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void pwb_Loaded(object sender, RoutedEventArgs e)
        {
            pwb.Clear();

        }

        private void pwb_LostFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Text = "";
            txtPassword.Text = pwb.Password;

        }
    }
}


using System.Windows;

namespace MiPrimerProyecto
{
    public partial class SuccessWindow : Window
    {
        public SuccessWindow(string usuario)
        {
            InitializeComponent();

            txtBienvenida.Text = "Has iniciado sesión como: " + usuario;
        }

        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();

            this.Close();
        }
    }
}


using System.Windows;

namespace MiPrimerProyecto
{
    public partial class MainWindow : Window
    {
        public MainWindow(string nombreUsuario)
        {
            InitializeComponent();

            txtNombreUsuario.Text = "👤  " + nombreUsuario;
        }

        // Botón para cerrar sesión y volver al login
        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();

            this.Close();
        }
    }
}

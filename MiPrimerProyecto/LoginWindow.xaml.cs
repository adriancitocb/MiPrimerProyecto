
using System.Windows;

namespace MiPrimerProyecto
{
    public partial class LoginWindow : Window
    {
        // Usuarios y contraseñas almacenados en memoria
        private string[,] usuarios =
        {
            { "admin", "GameStation123" },
            { "adrian", "GameStation456" },
            { "invitado", "Invitado123" }
        };

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Password;

            // 1. Comprobar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(contrasena))
            {
                txtMensaje.Text = "Introduce el usuario y la contraseña.";
                return;
            }

            // 2. Validar usuario y contraseña
            for (int i = 0; i < usuarios.GetLength(0); i++)
            {
                if (usuario == usuarios[i, 0] &&
                    contrasena == usuarios[i, 1])
                {
                    txtMensaje.Text = "";

                    // Abrir el catálogo de videojuegos
                    MainWindow ventana = new MainWindow();
                    ventana.Show();

                    // Cerrar el login
                    this.Close();
                    return;
                }
            }

            // 3. Credenciales incorrectas
            txtMensaje.Text = "Usuario o contraseña incorrectos.";
            txtContrasena.Clear();
            txtContrasena.Focus();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            // Cerrar toda la aplicación
            Application.Current.Shutdown();
        }
    }
}

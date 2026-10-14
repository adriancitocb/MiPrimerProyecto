
using System;
using System.Windows;
using System.Windows.Threading;

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

        // Contador de contraseñas incorrectas
        private int intentosFallidos = 0;

        // Temporizador del bloqueo
        private DispatcherTimer temporizadorBloqueo;

        public LoginWindow()
        {
            InitializeComponent();

            // Configurar el temporizador de 30 segundos
            temporizadorBloqueo = new DispatcherTimer();
            temporizadorBloqueo.Interval = TimeSpan.FromSeconds(30);
            temporizadorBloqueo.Tick += TemporizadorBloqueo_Tick;
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

            // 2. Buscar si el usuario existe
            bool usuarioExiste = false;

            for (int i = 0; i < usuarios.GetLength(0); i++)
            {
                if (usuario == usuarios[i, 0])
                {
                    usuarioExiste = true;

                    // Comprobar si la contraseña es correcta
                    if (contrasena == usuarios[i, 1])
                    {
                        txtMensaje.Text = "";

                        // Abrir el catálogo de videojuegos
                        MainWindow ventana = new MainWindow();
                        ventana.Show();

                        // Cerrar el login
                        this.Close();
                        return;
                    }

                    break;
                }
            }

            // 3. Mostrar el error correspondiente
            if (!usuarioExiste)
            {
                txtMensaje.Text = "El usuario introducido no existe.";
                txtContrasena.Clear();
                txtContrasena.Focus();
                return;
            }

            // 4. Contar los intentos con contraseña incorrecta
            intentosFallidos++;

            if (intentosFallidos >= 3)
            {
                txtMensaje.Text =
                    "Has superado los 3 intentos. Cuenta bloqueada temporalmente.";

                btnLogin.IsEnabled = false;

                // Iniciar el bloqueo temporal
                temporizadorBloqueo.Start();
                return;
            }

            txtMensaje.Text = "La contraseña introducida no es correcta.";
            txtContrasena.Clear();
            txtContrasena.Focus();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            // Cerrar toda la aplicación
            Application.Current.Shutdown();
        }

        private void TemporizadorBloqueo_Tick(object sender, EventArgs e)
        {
            // Detener el temporizador
            temporizadorBloqueo.Stop();

            // Reiniciar el contador y permitir nuevos intentos
            intentosFallidos = 0;
            btnLogin.IsEnabled = true;

            txtMensaje.Text = "Ya puedes volver a iniciar sesión.";
            txtContrasena.Clear();
            txtContrasena.Focus();
        }
    }
}

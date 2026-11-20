
using System;
using System.Windows;
using System.Windows.Threading;
using MySql.Data.MySqlClient;

namespace MiPrimerProyecto
{
    public partial class LoginWindow : Window
    {
        private int intentosFallidos = 0;
        private DispatcherTimer temporizadorBloqueo;

        public LoginWindow()
        {
            InitializeComponent();

            temporizadorBloqueo = new DispatcherTimer();
            temporizadorBloqueo.Interval = TimeSpan.FromSeconds(30);
            temporizadorBloqueo.Tick += TemporizadorBloqueo_Tick;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Password;

            if (string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(contrasena))
            {
                txtMensaje.Text = "Introduce el usuario y la contraseña.";
                return;
            }

            try
            {
                using (MySqlConnection conexion =
                    new DatabaseHelper().GetConnection())
                {
                    conexion.Open();

                    string consulta =
                        "SELECT password FROM Usuarios " +
                        "WHERE nombre_usuario = @usuario";

                    using (MySqlCommand comando =
                        new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@usuario", usuario);

                        object resultado = comando.ExecuteScalar();

                        if (resultado == null)
                        {
                            txtMensaje.Text =
                                "El usuario introducido no existe.";
                            txtContrasena.Clear();
                            txtContrasena.Focus();
                            return;
                        }

                        string passwordGuardada = resultado.ToString();

                        if (contrasena == passwordGuardada)
                        {
                            intentosFallidos = 0;
                            txtMensaje.Text = "";

                            // Enviar el nombre del usuario al catálogo
                            MainWindow ventana = new MainWindow(usuario);
                            ventana.Show();

                            this.Close();
                            return;
                        }
                    }
                }

                intentosFallidos++;

                if (intentosFallidos >= 3)
                {
                    txtMensaje.Text =
                        "Has superado los 3 intentos. Cuenta bloqueada temporalmente.";

                    btnLogin.IsEnabled = false;
                    temporizadorBloqueo.Start();
                    return;
                }

                txtMensaje.Text =
                    "La contraseña introducida no es correcta.";

                txtContrasena.Clear();
                txtContrasena.Focus();
            }
            catch (MySqlException)
            {
                txtMensaje.Text =
                    "No se pudo conectar con la base de datos. Comprueba que MySQL esté iniciado.";
            }
            catch (Exception)
            {
                txtMensaje.Text =
                    "Se ha producido un error inesperado al iniciar sesión.";
            }
        }

        private void BtnNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {
            RegistroWindow registro = new RegistroWindow();
            registro.ShowDialog();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TemporizadorBloqueo_Tick(object sender, EventArgs e)
        {
            temporizadorBloqueo.Stop();

            intentosFallidos = 0;
            btnLogin.IsEnabled = true;

            txtMensaje.Text = "Ya puedes volver a iniciar sesión.";
            txtContrasena.Clear();
            txtContrasena.Focus();
        }
    }
}

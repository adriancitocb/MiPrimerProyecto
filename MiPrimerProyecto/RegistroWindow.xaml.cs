
using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace MiPrimerProyecto
{
    public partial class RegistroWindow : Window
    {
        public RegistroWindow()
        {
            InitializeComponent();
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtNuevoUsuario.Text.Trim();
            string contrasena = txtNuevaContrasena.Password;

            if (string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show(
                    "Introduce el usuario y la contraseña.",
                    "GAMESTATION",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conexion =
                    new DatabaseHelper().GetConnection())
                {
                    conexion.Open();

                    string consulta =
                        "INSERT INTO Usuarios (nombre_usuario, password) " +
                        "VALUES (@usuario, @password)";

                    using (MySqlCommand comando =
                        new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@usuario", usuario);
                        comando.Parameters.AddWithValue("@password", contrasena);

                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Usuario registrado correctamente.",
                    "GAMESTATION",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                this.Close();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    MessageBox.Show(
                        "Ese nombre de usuario ya está registrado.",
                        "GAMESTATION",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    txtMensajeRegistro.Text =
                        "Ese nombre de usuario ya está registrado.";
                }
                else
                {
                    MessageBox.Show(
                        "Error MySQL " + ex.Number + ":\n" + ex.Message,
                        "GAMESTATION",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado:\n" + ex.Message,
                    "GAMESTATION",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

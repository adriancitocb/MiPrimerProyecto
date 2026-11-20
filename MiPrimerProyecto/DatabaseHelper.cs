
using MySql.Data.MySqlClient;

namespace MiPrimerProyecto
{
    internal class DatabaseHelper
    {
        private readonly string connectionString =
            "Server=localhost;Port=3306;Database=gamestation;Uid=root;Pwd=adrian2005;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}

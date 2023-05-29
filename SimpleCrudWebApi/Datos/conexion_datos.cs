using System.Data.SqlClient;

namespace SimpleCrudWebApi.Datos
{
    public class conexion_datos
    {
        private string connectionString = string.Empty;

        public conexion_datos()
        {
            var constructor = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            connectionString = constructor.GetSection("ConnectionStrings:connection_bd").Value;
        }

        public string cadenaSQL()
        {
            return connectionString;
        }

        public static SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection("data source = localhost; initial catalog = simplecruddb; Integrated Security = true;");
            connection.Open();
            return connection;
        }
    }
}

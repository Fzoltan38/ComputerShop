using MySql.Data.MySqlClient;

namespace ComputerShop
{
    internal class Connect
    {

        public MySqlConnection Connection;

        private string _host;
        private string _database;
        private string _user;
        private string _password;

        private string ConnectionString;

        public Connect()
        {

            _host = "127.0.0.1";
            _database = "computershop";
            _user = "root";
            _password = "";

            ConnectionString = $"SERVER={_host};DATABASE={_database};UID={_user};PASSWORD={_password};SslMode=None";
            Connection = new MySqlConnection(ConnectionString);

        }
    }
}

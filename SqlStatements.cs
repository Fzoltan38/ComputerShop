using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace ComputerShop
{
    internal class SqlStatements
    {
        Connect conn = new Connect();

        //User bejelentkezéséhez
        public bool LoginUser(string username, string userpassword)
        {
            try
            {
                conn.Connection.Open();

                string sql = "SELECT Id FROM users  WHERE `UserName`=@username AND `Password`=@password;";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", userpassword);

                MySqlDataReader dr = cmd.ExecuteReader();
                bool isValid = dr.Read();

                dr.Close();
                conn.Connection.Close();
                return isValid;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        //User regisztrálásához
        public string RegisterUser(string username, string password, string fullname, string email)
        {
            try
            {
                conn.Connection.Open();

                string sql = "INSERT INTO `users`(`UserName`, `Password`, `FullName`, `Email`) VALUES (@username,@password,@fullname,@email)";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@fullname", fullname);
                cmd.Parameters.AddWithValue("@email", email);

                cmd.ExecuteNonQuery();

                conn.Connection.Close();

                return "Sikeres regisztráció";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }

        }

        //Userek megjelenítése datagrid-ben
        public DataView GetAllUser()
        {
            try
            {
                conn.Connection.Open();

                string sql = "SELECT * FROM users";

                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn.Connection);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                conn.Connection.Close();

                return dt.DefaultView;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }

        //User törlése
        public void DeleteUser(object id)
        {
            try
            {
                conn.Connection.Open();

                string sql = "DELETE FROM users WHERE Id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                conn.Connection.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //User adatok módisítása

        public void UpdateUser(object Row)
        {
            try
            {
                conn.Connection.Open();

                string sql = "UPDATE `users` SET `UserName`=@username,`Password`=@password,`FullName`=@fullname,`Email`=@email,`RegDate`=@date WHERE Id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

                var usr = Row.GetType().GetProperties();

                cmd.Parameters.AddWithValue("@username", usr[1].GetValue(Row));
                cmd.Parameters.AddWithValue("@password", usr[2].GetValue(Row));
                cmd.Parameters.AddWithValue("@fullname", usr[3].GetValue(Row));
                cmd.Parameters.AddWithValue("@email", usr[4].GetValue(Row));
                cmd.Parameters.AddWithValue("@date", usr[5].GetValue(Row));
                cmd.Parameters.AddWithValue("@id", usr[0].GetValue(Row));

                cmd.ExecuteNonQuery();

                conn.Connection.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}

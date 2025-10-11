using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
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

                string sql = "SELECT * FROM users  WHERE `UserName`= @username;";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

                cmd.Parameters.AddWithValue("@username", username);

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string storedHash = dr.GetString("Password");
                    string storedSalt = dr.GetString("Salt");
                    string computeHash = ComputeHmacHash256(userpassword, storedSalt);
                    conn.Connection.Close();
                    return storedHash == computeHash;
                }

                conn.Connection.Close();
                return false;
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
                string salt = GenerateSalt();
                string hashedPassword = ComputeHmacHash256(password, salt);
                conn.Connection.Open();

                string sql = "INSERT INTO `users`(`UserName`, `Password`, `Salt`, `FullName`, `Email`) VALUES (@username,@password,@salt,@fullname,@email)";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@fullname", fullname);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@salt", salt);

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

        public string GenerateSalt()
        {
            byte[] salt = new byte[16];

            using (var rnd = RandomNumberGenerator.Create())
            {
                rnd.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public string ComputeHmacHash256(string password, string salt)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(salt)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }
    }
}

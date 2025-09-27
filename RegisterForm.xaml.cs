using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Page
    {
        Connect conn = new Connect();
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(registerUser(userNameTextBox.Text, userPasswordTextBox.Password, userFullNameTextBox.Text, userEmailTextBox.Text));

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private string registerUser(string username, string password, string fullname, string email)
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
    }
}

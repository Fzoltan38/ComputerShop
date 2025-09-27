using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private MainWindow _mainWindow;
        public Page1(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LoginUser(userNameTextBox.Text, userPasswordTextBox.Password))
                {
                    largeWindow.Text = "Regisztrált tag.";
                }
                else
                {
                    largeWindow.Text = "Még nem regisztrált tag.";
                }
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private bool LoginUser(string username, string userpassword)
        {
            Connect conn = new Connect();

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

        private void registerLink_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.Navigate(new RegisterForm());
        }
    }
}

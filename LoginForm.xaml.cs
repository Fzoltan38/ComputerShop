using System.Windows;
using System.Windows.Controls;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Page
    {
        private MainWindow _mainWindow;
        SqlStatements sql = new SqlStatements();
        public LoginForm(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sql.LoginUser(userNameTextBox.Text, userPasswordTextBox.Password))
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
        private void registerLink_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.Navigate(new RegisterForm());
        }
    }
}

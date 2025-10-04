using System.Windows;
using System.Windows.Controls;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Page
    {
        private MainWindow _mainWindow;
        SqlStatements sql = new SqlStatements();
        public RegisterForm(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(sql.RegisterUser(userNameTextBox.Text, userPasswordTextBox.Password, userFullNameTextBox.Text, userEmailTextBox.Text));
                _mainWindow.MainFrame.Navigate(new LoginForm(_mainWindow));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


    }
}

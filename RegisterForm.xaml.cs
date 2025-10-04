using System.Windows;
using System.Windows.Controls;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Page
    {
        SqlStatements sql = new SqlStatements();
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(sql.RegisterUser(userNameTextBox.Text, userPasswordTextBox.Password, userFullNameTextBox.Text, userEmailTextBox.Text));

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


    }
}

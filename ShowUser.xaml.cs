using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for ShowUser.xaml
    /// </summary>
    public partial class ShowUser : Page
    {
        SqlStatements sql = new SqlStatements();
        public ShowUser()
        {
            InitializeComponent();
            userDataGrid.ItemsSource = sql.GetAllUser();

        }

        private void userDeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (userDataGrid.SelectedItem is DataRowView item)
            {
                MessageBox.Show(item["UserName"].ToString());
            }

        }
    }
}

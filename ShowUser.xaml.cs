using System.Data;
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
                sql.DeleteUser(item["Id"]);
                userDataGrid.ItemsSource = sql.GetAllUser();

            }

        }

        private void userUpdteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (userDataGrid.SelectedItem is DataRowView item)
            {
                var user = new
                {
                    Id = item["Id"],
                    UserName = item["UserName"],
                    Password = item["Password"],
                    FulName = item["FullName"],
                    Email = item["Email"],
                    RegDate = item["RegDate"]
                };

                sql.UpdateUser(user);
                userDataGrid.ItemsSource = sql.GetAllUser();
            }
        }
    }
}

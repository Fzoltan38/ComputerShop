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
    }
}

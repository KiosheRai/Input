using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Input
{
    /// <summary>
    /// Логика взаимодействия для CustomerShowOrder.xaml
    /// </summary>
    public partial class CustomerShowOrder : Page
    {
        private string Login;
        private string Id;

        public CustomerShowOrder(string login, string id)
        {
            this.Login = login;
            this.Id = id;
            InitializeComponent();
            ShowInfo();
        }

        private void ShowInfo()
        {
            DataTable table = SQLbase.Select($"select * from Orders where id = {Id}");

            typeBox.Text = table.Rows[0][2].ToString();
            descBox.Text = table.Rows[0][3].ToString();
            priceBox.Text = table.Rows[0][4].ToString();
            statusBox.Text = table.Rows[0][5].ToString();
            progBox.Text = table.Rows[0][6].ToString();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);

            CustomerPage nextPage = new CustomerPage(Login);
            nav.Navigate(nextPage);
        }
    }
}

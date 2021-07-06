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
    /// Логика взаимодействия для CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        private string Login;

        public CustomerPage(string login)
        {
            this.Login = login;
            InitializeComponent();
            ShowList();
        }

        private void ShowList()
        {
            DataTable table = SQLbase.Select($"select id, title, price, status, programmer from Orders where customer = N'{Login}'");

            listGoods.ItemsSource = table.DefaultView;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            int x = listGoods.SelectedIndex;

            if (x == -1)
            {
                ButtonDelete.ToolTip = "Выберите элемент!";
                ButtonDelete.Foreground = Brushes.Red;
                return;
            }
            else
            {
                ButtonDelete.ToolTip = "";
                ButtonDelete.Foreground = Brushes.LightGreen;
            }

            DataRowView i = (DataRowView)listGoods.Items[x];
            SQLbase.Insert($"delete Orders where id = {i.Row.ItemArray[0].ToString()}");

            ShowList();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);

            CustomerAddPage nextPage = new CustomerAddPage(Login);
            nav.Navigate(nextPage);
        }

        private void Show(object sender, RoutedEventArgs e)
        {
            int x = listGoods.SelectedIndex;

            if (x == -1)
            {
                ButtonShow.ToolTip = "Выберите элемент!";
                ButtonShow.Foreground = Brushes.Red;
                return;
            }
            else
            {
                ButtonShow.ToolTip = "";
                ButtonShow.Foreground = Brushes.LightGreen;
            }

            DataRowView i = (DataRowView)listGoods.Items[x];

            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);

            CustomerShowOrder nextPage = new CustomerShowOrder(Login, i.Row.ItemArray[0].ToString());
            nav.Navigate(nextPage);
        }
    }
}

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
    /// Логика взаимодействия для ProgrammerShowOrder.xaml
    /// </summary>
    public partial class ProgrammerShowOrder : Page
    {
        private string Login;
        private string Id;

        public ProgrammerShowOrder(string login, string id)
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
            priceBox.Text = String.Format("{0:C}", table.Rows[0][4]);
            statusBox.Text = table.Rows[0][5].ToString();
            progBox.Text = table.Rows[0][6].ToString();
        }

        private void TakeOrder(object sender, RoutedEventArgs e)
        {
            SQLbase.Insert($"update Orders set status = 'Занят' where id = '{Id}'");
            SQLbase.Insert($"update Orders set programmer = '{Login}' where id = '{Id}'");

            ShowInfo();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);

            ProgrammerPAge nextPage = new ProgrammerPAge(Login);
            nav.Navigate(nextPage);
        }
    }
}

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
    /// Логика взаимодействия для ProgrammerPAge.xaml
    /// </summary>
    public partial class ProgrammerPAge : Page
    {
        private string Login;
        private bool showAll;

        public ProgrammerPAge(string login)
        {
            showAll = true;
            this.Login = login;
            InitializeComponent();
            ShowList();
        }

        private void ShowList()
        {
            DataTable table;

            if (showAll)
            {
               table = SQLbase.Select($"select id, title, price, status, programmer from Orders where status = N'Открытый' or programmer = '{Login}'");
            }
            else
            {
               table = SQLbase.Select($"select id, title, price, status, programmer from Orders where programmer = '{Login}'");
            }
            listGoods.ItemsSource = table.DefaultView;
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

            ProgrammerShowOrder nextPage = new ProgrammerShowOrder(Login, i.Row.ItemArray[0].ToString());
            nav.Navigate(nextPage);
        }

        private void TakeOrder(object sender, RoutedEventArgs e)
        {
            int x = listGoods.SelectedIndex;

            if (x == -1)
            {
                TakeOrderButton.ToolTip = "Выберите элемент!";
                TakeOrderButton.Foreground = Brushes.Red;
                return;
            }
            else
            {
                TakeOrderButton.ToolTip = "";
                TakeOrderButton.Foreground = Brushes.LightGreen;
            }

            DataRowView i = (DataRowView)listGoods.Items[x];

            SQLbase.Insert($"update Orders set status = 'Занят' where id = '{i.Row.ItemArray[0].ToString()}'");
            SQLbase.Insert($"update Orders set programmer = '{Login}' where id = '{i.Row.ItemArray[0].ToString()}'");

            ShowList();
        }

        private void ShowAll(object sender, RoutedEventArgs e)
        {
            if (showAll)
            {
                ButtonShowAll.Content = "Отобразить всё";
                showAll = false;
            }
            else
            {
                ButtonShowAll.Content = "Отобразить моё";
                showAll = true;
            }

            ShowList();
        }
    }
}

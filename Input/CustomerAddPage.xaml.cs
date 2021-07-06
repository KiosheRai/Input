using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CustomerAddPage.xaml
    /// </summary>
    public partial class CustomerAddPage : Page
    {
        private string login;

        public CustomerAddPage(string login)
        {
            this.login = login;
            InitializeComponent();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);

            CustomerPage nextPage = new CustomerPage(login);
            nav.Navigate(nextPage);
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            string type = typeBox.Text;
            string desc = descBox.Text;
            string price = priceBox.Text;

            bool isGood = true;
            if (type.Length < 1)
            {
                isGood = false;
                typeBox.ToolTip = "Введите тип приложения!";
                typeBox.Foreground = Brushes.Red;
            }
            else
            {
                typeBox.ToolTip = " ";
                typeBox.Foreground = Brushes.Black;
            }

            if (desc.Length < 1)
            {
                isGood = false;
                descBox.ToolTip = "Введите описание!";
                descBox.Foreground = Brushes.Red;
            }
            else
            {
                descBox.ToolTip = " ";
                descBox.Foreground = Brushes.Black;
            }

            if (price.Length < 1)
            {
                isGood = false;
                priceBox.ToolTip = "Введите бюджет приложения!";
                priceBox.Foreground = Brushes.Red;
            }
            else
            {
                if(Decimal.TryParse(price, out decimal temp))
                {
                    if (temp <= 0)
                    {
                        isGood = false;
                        priceBox.ToolTip = "Введите положительный бюджет приложения!";
                        priceBox.Foreground = Brushes.Red;
                    }
                    else
                    {
                        priceBox.ToolTip = " ";
                        priceBox.Foreground = Brushes.Black;
                    }
                }
                else
                {
                    isGood = false;
                    priceBox.ToolTip = "Неверный формат ввода!";
                    priceBox.Foreground = Brushes.Red;
                }

               
            }

            if (isGood)
            {
                typeBox.Text = "";
                descBox.Text = "";
                priceBox.Text = "";

                SQLbase.Insert($"insert into Orders(customer,title,description,price,status) values ('{login}', '{type}', '{desc}', {price}, 'Открытый')");

                AddButton.Foreground = Brushes.Green;
                AddButton.ToolTip = "Заказ создан";
            }
            else
            {
                AddButton.Foreground = Brushes.White;
                AddButton.ToolTip = "";
            }
        }
    }
}

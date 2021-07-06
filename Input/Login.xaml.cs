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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        Criterion crt;

        public Login()
        {
            InitializeComponent();
        }


        private enum Criterion
        {
            customer,
            programmer
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            string s = pressed.Content.ToString();

            if (s == "Клиент")
            {
                crt = Criterion.customer;
                return;
            }
            if (s == "Программист")
            {
                crt = Criterion.programmer;
                return;
            }
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            string login = loginBox.Text.Trim().ToUpper();
            string password1 = firstPassBox.Password.Trim();

            bool isGood = true;
            if (login.Length < 1)
            {
                isGood = false;
                loginBox.ToolTip = "Введите логин!";
                loginBox.Foreground = Brushes.Red;
            }
            else
            {
                loginBox.ToolTip = " ";
                loginBox.Foreground = Brushes.Black;
            }

            if (password1.Length < 1)
            {
                isGood = false;
                firstPassBox.ToolTip = "Введите пароль!";
                firstPassBox.Foreground = Brushes.Red;
            }
            else
            {
                firstPassBox.ToolTip = " ";
                firstPassBox.Foreground = Brushes.Black;
            }

            if (isGood == true)
            {
                if(crt == Criterion.customer)
                {
                    DataTable table = SQLbase.Select($"select * from Customer where login = '{login}'");

                    if (table.Rows.Count > 0)
                    {
                        loginBox.ToolTip = " ";
                        loginBox.Foreground = Brushes.Black;
                        table = SQLbase.Select($"select * from Customer where login = '{login}' and pass = '{password1}'");
                        if (table.Rows.Count > 0)
                        {
                            MessageBox.Show("Блять1");
                            //Goods s = new Goods(login);
                            //this.Close();

                            //s.Show();
                        }
                        else
                        {
                            firstPassBox.ToolTip = "Неправильный пароль!";
                            firstPassBox.Foreground = Brushes.Red;
                        }
                    }
                    else
                    {
                        loginBox.ToolTip = "Такого пользователя не существует!";
                        loginBox.Foreground = Brushes.Red;
                    }
                }

                if (crt == Criterion.programmer)
                {
                    DataTable table = SQLbase.Select($"select * from Programmer where login = '{login}'");

                    if (table.Rows.Count > 0)
                    {
                        loginBox.ToolTip = " ";
                        loginBox.Foreground = Brushes.Black;
                        table = SQLbase.Select($"select * from Programmer where login = '{login}' and pass = '{password1}'");
                        if (table.Rows.Count > 0)
                        {
                            MessageBox.Show("Блять");
                            //Goods s = new Goods(login);
                            //this.Close();

                            //s.Show();
                        }
                        else
                        {
                            firstPassBox.ToolTip = "Неправильный пароль!";
                            firstPassBox.Foreground = Brushes.Red;
                        }
                    }
                    else
                    {
                        loginBox.ToolTip = "Такого пользователя не существует!";
                        loginBox.Foreground = Brushes.Red;
                    }
                }

            }
        }

        private void GoToReg(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Register.xaml", UriKind.Relative));
        }
    }
}

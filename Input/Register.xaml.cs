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
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        Criterion crt;

        public Register()
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

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = loginBox.Text.Trim().ToUpper();
            string[] name = nameBox.Text.Trim().Split(' ');
            string password1 = firstPassBox.Password.Trim();
            string password2 = secondPassBox.Password.Trim();

            bool isGood = true;
            //Проверка поля логина
            if (login.Length < 3 && login.Length > 20)
            {
                isGood = false;
                loginBox.ToolTip = "Длина логина должна быть от 3 до 20 букв!";
                loginBox.Foreground = Brushes.Red;
            }
            else
            {
                bool stop = false;

                foreach (char x in login)
                {
                    if (Char.IsDigit(x))
                    {
                        stop = true;
                        break;
                    }

                    if (Convert.ToInt16(x) < 0 || Convert.ToInt16(x) > 128)
                    {
                        stop = true;
                        break;
                    }


                }

                if (stop)
                {
                    isGood = false;
                    loginBox.ToolTip = "Недопустимый ввод символов!";
                    loginBox.Foreground = Brushes.Red;
                }
                else
                {
                    if (crt == Criterion.customer)
                    {
                        DataTable table = SQLbase.Select($"select * from Customer where login = '{login}'");
                        if (table.Rows.Count > 0)
                        {
                            isGood = false;
                            loginBox.ToolTip = "Такой пользователь уже существует!";
                            loginBox.Foreground = Brushes.Red;
                        }
                        else
                        {
                            loginBox.ToolTip = " ";
                            loginBox.Foreground = Brushes.Black;
                        }
                    }

                    if(crt == Criterion.programmer)
                    {
                        DataTable table = SQLbase.Select($"select * from Programmer where login = '{login}'");
                        if (table.Rows.Count > 0)
                        {
                            isGood = false;
                            loginBox.ToolTip = "Такой пользователь уже существует!";
                            loginBox.Foreground = Brushes.Red;
                        }
                        else
                        {
                            loginBox.ToolTip = " ";
                            loginBox.Foreground = Brushes.Black;
                        }
                    }
                }
            }

            //Поле фио
            if (!(name.Length == 2 || name.Length == 3))
            {
                isGood = false;
                nameBox.ToolTip = "Требуется полное написание ФИО!";
                nameBox.Foreground = Brushes.Red;
            }
            else
            {
                bool stop = false;

                foreach (char x in nameBox.Text)
                {
                    if (Char.IsDigit(x))
                    {
                        stop = true;
                        break;
                    }
                }

                if (stop)
                {
                    isGood = false;
                    nameBox.ToolTip = "Недопустимый ввод символов!";
                    nameBox.Foreground = Brushes.Red;
                }
                else
                {
                    nameBox.ToolTip = "";
                    nameBox.Foreground = Brushes.Black;
                }
            }

            //Проверка пароля
            if (password1.Length < 4 && password1.Length > 16)
            {
                isGood = false;
                firstPassBox.ToolTip = "Длина пароля должна быть от 4 до 16 символов!";
                firstPassBox.Foreground = Brushes.Red;
            }
            else
            {
                firstPassBox.ToolTip = "";
                firstPassBox.Foreground = Brushes.Black;
            }

            //Подтверждение пароля
            if (password1 != password2)
            {
                isGood = false;
                secondPassBox.ToolTip = "Пароли не совпадают";
                secondPassBox.Foreground = Brushes.Red;
            }
            else
            {
                secondPassBox.ToolTip = "";
                secondPassBox.Foreground = Brushes.Black;
            }

            if (isGood == true)
            {
                if (crt == Criterion.customer)
                {
                    SQLbase.Insert($"insert into Customer(login, name, pass) values (N'{login}',N'{name}',N'{password1}')");
                }
                if(crt == Criterion.programmer)
                {
                    SQLbase.Insert($"insert into Programmer(login, name, pass) values (N'{login}',N'{name}',N'{password1}')");
                }

                NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
            }
        }

        private void ButtonLoginGo(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }
    }
}

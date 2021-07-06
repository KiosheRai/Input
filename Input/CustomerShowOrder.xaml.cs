using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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
using Word = Microsoft.Office.Interop.Word;

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

        private void ReplaceWordStub(String stubToReplace, String Text, Word.Document word)
        {
            var range = word.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: Text);
        }

        private void ReportGo()
        {
            DataTable table = SQLbase.Select($"select * from Orders where id = {Id}");

            try
            {
                var word = new Word.Application();
                word.Visible = false;

                var worddoc = word.Documents.Open($"{Environment.CurrentDirectory}/ReportMaket.DOCX");

                ReplaceWordStub("{id}", table.Rows[0][0].ToString(), worddoc);
                ReplaceWordStub("{customer}", table.Rows[0][1].ToString(), worddoc);
                ReplaceWordStub("{title}", table.Rows[0][2].ToString(), worddoc);
                ReplaceWordStub("{description}", table.Rows[0][3].ToString(), worddoc);
                ReplaceWordStub("{price}", table.Rows[0][4].ToString(), worddoc);
                ReplaceWordStub("{programmer}", table.Rows[0][6].ToString(), worddoc);

                worddoc.SaveAs2($"{Environment.CurrentDirectory}/report.docx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Report(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(ReportGo);
            t.Start();
            MessageBox.Show("Отчёт создан!");
        }
    }
}

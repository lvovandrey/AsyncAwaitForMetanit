using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static int Rnd;

        void Calc()
        {
            Logging("Зашли в Calc() - " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n");
            Thread.Sleep(3000);
            Random r = new Random();
            Rnd = r.Next(0, 1000);
        }

        async void WriteAsync()
        {
            Logging("Зашли в WriteAsync() - " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n" );
            await Task.Run(() => { Calc(); });

            //Здесь "оставшаяся часть" метода выполняется в первичном потоке.
            Logging("После выполнения таски в WriteAsync() - " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n");

            //собственно благодаря тому что мы находимся в первичном потоке мы далее можем не заморачиваться с Dispatcher.Invoke
           Logging(Rnd.ToString());
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Logging("Зашли в Button_Click - " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n");
            await Task.Run(() =>
            {
                WriteAsync();
            });
            Logging("Завершили Button_Click - " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n");
        }



        void Logging(string text)
        {
            Debug.Write(text);
            Dispatcher.Invoke(() => { Log.Text += text; });
        }
    }
}

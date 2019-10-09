using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
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
            txt.Text = Rnd.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Logging("Зашли в Button_Click - " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n");
            WriteAsync();
            Logging("Завершили Button_Click - " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n");
        }

        void Logging(string text)
        {
            Log.Invoke((MethodInvoker)(() => Log.Text += text+ Environment.NewLine)); 
        }


    }
}

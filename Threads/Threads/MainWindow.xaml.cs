using System;
using System.Collections.Generic;
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

namespace Threads
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Arbeiter a = new Arbeiter();
        Arbeiter b = new Arbeiter();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            a.Start();
            b.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            a.Stop();
            b.Stop();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            label.Content = a.HoleSumme() + " " + b.HoleSumme();
        }
    }

    class Arbeiter
    {
        Thread thread;

        public void Start() // TODO: Was passiert, wenn 2 x gedrückt?
        {
            thread = new Thread(Arbeite);
            thread.IsBackground = true;
            thread.Start();
        }

        bool sollStoppen;
        object syncFürStoppen = new object();
        public void Stop() // TODO: Was passiert, wenn 2 x gedrückt?
        {
            lock (syncFürStoppen)
            {
                sollStoppen = true;
            }
            thread.Join();
        }

        double summe;
        object syncFürSumme = new object();

        public double HoleSumme()
        {
            lock (syncFürSumme)
            {
                return summe;
            }
        }

        void Arbeite()
        {
            for (long i = 0; i < 1000000000000; i++)
            {
                lock (syncFürSumme)
                {
                    summe += Math.Abs(Math.Sin(i));
                }
                lock (syncFürStoppen)
                {
                    if (sollStoppen)
                    {
                        break;
                    }
                }
            }
        }
    }
}

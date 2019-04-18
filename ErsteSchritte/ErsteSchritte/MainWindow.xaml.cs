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
using System.Windows.Threading;

namespace ErsteSchritte
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            StelleFarbeEin(phase);
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += PhaseWeiterschalten;
            timer.Start();

            // zur Wiederholung
            short a = 13 + 42;
            int b = 42;
            int b1 = (int)42.1234;
            long c = 42;
            bool d = false;
            bool e = (a > 23);
            float f = (float)1.23;
            double g = 1.23456;
            double h = double.PositiveInfinity;
            double i = Math.Sqrt(h);
            double j = 0.0 * h;
            double k = 1.0 / -0.0;

            int[] m = { 13, 23, 42, -100 };
            // rechteckiges zweidimensionales Array
            int[,] n = { { 13, 42 }, { 22, 43 }, { 100, 101 } };
            int p = n[0, 1];
            // "jagged" zweidimensionales Array
            int[][] q = { new int[] { 13, 42, 10000 }, new int[] { 22 }, new int[] { 100, 101 } };
            int r = q[2][1];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button geklickt");
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Title = "Maus war über Button";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MwStAusrechnen();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            MwStAusrechnen();
        }

        private void MwStAusrechnen()
        {
            // Bei Aufbau des Fensters werden schon Ereignisse ausgelöst, 
            // noch bevor alle Komponenten bereit stehen.
            // In diesem Fall können wir noch nichts tun. Also:
            if (radioButton7 == null || textBox.Text == null || textBlock == null)
            {
                return; 
            }

            double x = double.Parse(textBox.Text);
            if (radioButton7.IsChecked == true)
            {
                x *= 1.07;
            }
            else
            {
                x *= 1.19;
            }
            textBlock.Text = x.ToString();
        }

        enum Ampelphase { Grün, Gelb, Rot, RotGelb }

        // zum Vergleich:
        // int a = 42;
        Ampelphase phase = Ampelphase.Grün;

        DispatcherTimer timer = new DispatcherTimer();

        private void PhaseWeiterschalten(object sender, EventArgs e)
        {
            switch (phase)
            {
                case Ampelphase.Grün:
                    phase = Ampelphase.Gelb;
                    break;
                case Ampelphase.Gelb:
                    phase = Ampelphase.Rot;
                    break;
                case Ampelphase.Rot:
                    phase = Ampelphase.RotGelb;
                    break;
                case Ampelphase.RotGelb:
                    phase = Ampelphase.Grün;
                    break;
                default:
                    break;
            }
            StelleFarbeEin(phase);
        }

        private void StelleFarbeEin(Ampelphase phase)
        {
            ellipseUnten.Fill = ellipseMitte.Fill = ellipseOben.Fill = Brushes.Black;
            switch (phase)
            {
                case Ampelphase.Grün:
                    ellipseUnten.Fill = Brushes.Green;
                    break;
                case Ampelphase.Gelb:
                    ellipseMitte.Fill = Brushes.Yellow;
                    break;
                case Ampelphase.Rot:
                    ellipseOben.Fill = Brushes.Red;
                    break;
                case Ampelphase.RotGelb:
                    ellipseMitte.Fill = Brushes.Yellow;
                    ellipseOben.Fill = Brushes.Red;
                    break;
                default:
                    break;
            }
        }
    }
}

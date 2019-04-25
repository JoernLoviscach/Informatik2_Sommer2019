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

namespace Heizlast
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Wand w1 = new Wand(4.0, 3.0, 0.3);
            Wand w2 = new Wand(5.0, 3.0, 0.3);
            Wand w3 = new Wand(6.0, 3.0, 0.3);
            Wand w4 = new Wand(6.0, 3.0, 1.0);
            Wand w5 = new Wand(6.0, 3.0, 0.3);
            Wand w6 = new Wand(4.0, 3.0, 0.3);
            Wand w7 = new Wand(5.0, 3.0, 0.3);
            Raum r1 = new Raum(new Wand[] { w1, w3, w4, w6 }, 22.0, "Schlafzimmmer");
            Raum r2 = new Raum(new Wand[] { w2, w4, w5, w7 }, 24.0, "Wohnzimmmer");
            Raum r3 = new Raum(new Wand[] { w1, w2, w3, w5, w6, w7 }, 10.0, "Außenraum");
            double v1 = r1.BerechneHeizbedarf();
        }
    }

    class Raum
    {
        Wand[] Wände;
        public double Lufttemperatur; // TODO: keine öffentlichen Felder
        string Name;

        public Raum(Wand[] w, double lt, string n)
        {
            Wände = w;
            Lufttemperatur = lt;
            Name = n;

            for (int i = 0; i < Wände.Length; i++)
            {
                Wände[i].SetzeRaum(this);
            }
        }

        public double BerechneHeizbedarf()
        {
            double summe = 0.0;
            for (int i = 0; i < Wände.Length; i++)
            {
                summe += Wände[i].BerechneVerlust();
            }
            return summe;
        }
    }

    class Wand
    {
        double Breite;
        double Höhe;
        double UWert;
        Raum Raum1;
        Raum Raum2;

        public Wand(double b, double h, double u)
        {
            Breite = b;
            Höhe = h;
            UWert = u;
        }

        public double BerechneVerlust()
        {
            // TODO: Vorzeichen ist ggf. falsch
            double tDiff = Raum1.Lufttemperatur - Raum2.Lufttemperatur;
            return Breite * Höhe * UWert * tDiff;
        }

        public void SetzeRaum(Raum raum)
        {
            if(Raum1 == null)
            {
                Raum1 = raum;
            }
            else
            {
                Raum2 = raum;
            }
        }
    }
}

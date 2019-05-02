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
            // TODO: Ein paar mehr Fenster wären schön.
            Fenster f1 = new Fenster(1.5, 1.5, 1.3);
            Wand w1 = new Wand(new Fenster[] { f1 }, 4.0, 3.0, 0.3);
            Wand w2 = new Wand(new Fenster[0], 5.0, 3.0, 0.3);
            Wand w3 = new Wand(new Fenster[0], 6.0, 3.0, 0.3);
            Wand w4 = new Wand(new Fenster[0], 6.0, 3.0, 1.0);
            Wand w5 = new Wand(new Fenster[0], 6.0, 3.0, 0.3);
            Wand w6 = new Wand(new Fenster[0], 4.0, 3.0, 0.3);
            Wand w7 = new Wand(new Fenster[0], 5.0, 3.0, 0.3);
            Raum r1 = new Raum(new Wand[] { w1, w3, w4, w6 }, 22.0, "Schlafzimmmer");
            Raum r2 = new Raum(new Wand[] { w2, w4, w5, w7 }, 24.0, "Wohnzimmmer");
            Raum r3 = new Raum(new Wand[] { w1, w2, w3, w5, w6, w7 }, 10.0, "Außenraum");
            double v1 = r1.BerechneHeizbedarf();
        }
    }

    class Fenster
    {
        double breite;
        public double Breite { get { return breite; } }
        double höhe;
        public double Höhe { get { return höhe; } }
        double uWert;
        public double UWert { get { return uWert; } }

        public Fenster(double breite, double höhe, double uWert)
        {
            this.breite = breite;
            this.höhe = höhe;
            this.uWert = uWert;
        }
    }

    class Raum
    {
        Wand[] wände;
        double lufttemperatur;
        public double Lufttemperatur
        {
            get { return lufttemperatur; }
            //set { lufttemperatur = value; }
        }

        string name;

        public Raum(Wand[] wände, double lufttemperatur, string name)
        {
            this.wände = wände;
            this.lufttemperatur = lufttemperatur;
            this.name = name;

            for (int i = 0; i < wände.Length; i++)
            {
                wände[i].SetzeRaum(this);
            }
        }

        public double BerechneHeizbedarf()
        {
            double summe = 0.0;
            for (int i = 0; i < wände.Length; i++)
            {
                summe += wände[i].BerechneVerlustAus(this);
            }
            return summe;
        }
    }

    class Wand
    {
        double breite;
        double höhe;
        double uWert;
        Raum raumA;
        Raum raumB;
        Fenster[] fenster;

        public Wand(Fenster[] fenster, double breite, double höhe, double uWert)
        {
            this.fenster = fenster;
            this.breite = breite;
            this.höhe = höhe;
            this.uWert = uWert;
        }

        // Wärme, die aus Raum r strömt, wird positiv gerechnet.
        public double BerechneVerlustAus(Raum r)
        {
            double tDiff;
            if (r == raumA)
            {
                tDiff = raumA.Lufttemperatur - raumB.Lufttemperatur;
            }
            else
            {
                tDiff = raumB.Lufttemperatur - raumA.Lufttemperatur;
            }

            double fenstergesamtfläche = 0.0;
            double wärmeverlustkoeffizientFenster = 0.0;
            for (int i = 0; i < fenster.Length; i++)
            {
                double fläche = fenster[i].Breite * fenster[i].Höhe;
                fenstergesamtfläche += fläche;
                wärmeverlustkoeffizientFenster += fläche * fenster[i].UWert;
            }

            return ((breite * höhe - fenstergesamtfläche) * uWert
                        + wärmeverlustkoeffizientFenster) * tDiff;
        }

        public void SetzeRaum(Raum raum)
        {
            if(raumA == null)
            {
                raumA = raum;
            }
            else
            {
                raumB = raum;
            }
        }
    }
}

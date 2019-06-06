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

namespace ZiehenMitDerMausUndSchnittpunktBerechnen
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Random r = new Random();
            // TODO: Spätere Breite und Höhe der Canvas benutzen!
            // TODO: Schleife statt Copy & Paste
            Canvas.SetLeft(Anfasser1, r.NextDouble() * (Width - Anfasser1.Width));
            Canvas.SetLeft(Anfasser2, r.NextDouble() * (Width - Anfasser1.Width));
            Canvas.SetLeft(Anfasser3, r.NextDouble() * (Width - Anfasser1.Width));
            Canvas.SetLeft(Anfasser4, r.NextDouble() * (Width - Anfasser1.Width));
            Canvas.SetTop(Anfasser1, r.NextDouble() * (Height - Anfasser1.Height));
            Canvas.SetTop(Anfasser2, r.NextDouble() * (Height - Anfasser1.Height));
            Canvas.SetTop(Anfasser3, r.NextDouble() * (Height - Anfasser1.Height));
            Canvas.SetTop(Anfasser4, r.NextDouble() * (Height - Anfasser1.Height));

            JustiereGrafik();
        }

        void JustiereGrafik()
        {
            // TODO: Schleife statt Copy & Paste
            double x1 = Canvas.GetLeft(Anfasser1) + 0.5 * Anfasser1.Width;
            double x2 = Canvas.GetLeft(Anfasser2) + 0.5 * Anfasser2.Width;
            double x3 = Canvas.GetLeft(Anfasser3) + 0.5 * Anfasser3.Width;
            double x4 = Canvas.GetLeft(Anfasser4) + 0.5 * Anfasser4.Width;
            double y1 = Canvas.GetTop(Anfasser1) + 0.5 * Anfasser1.Height;
            double y2 = Canvas.GetTop(Anfasser2) + 0.5 * Anfasser2.Height;
            double y3 = Canvas.GetTop(Anfasser3) + 0.5 * Anfasser3.Height;
            double y4 = Canvas.GetTop(Anfasser4) + 0.5 * Anfasser4.Height;
            Linie1.X1 = x1;
            Linie1.Y1 = y1;
            Linie1.X2 = x2;
            Linie1.Y2 = y2;
            Linie2.X1 = x3;
            Linie2.Y1 = y3;
            Linie2.X2 = x4;
            Linie2.Y2 = y4;
            try
            {
                double nenner = (x1 - x2) * (y4 - y3) - (y1 - y2) * (x4 - x3);
                if(Math.Abs(nenner) < 1e-10)
                {
                    throw new ApplicationException("Koeffizientendeterminante ist fast null.");
                }
                double λ = ((x4 - x2) * (y4 - y3) - (y4 - y2) * (x4 - x3)) / nenner;
                if(λ > 1 || λ < 0)
                {
                    throw new ApplicationException("λ ist außerhalb des zulässigen Bereichs.");
                }
                double µ = ((x1 - x2) * (y4 - y2) - (y1 - y2) * (x4 - x2)) / nenner;
                if (µ > 1 || µ < 0)
                {
                    throw new ApplicationException("µ ist außerhalb des zulässigen Bereichs.");
                }
                double xs = λ * x1 + (1 - λ) * x2;
                double ys = λ * y1 + (1 - λ) * y2;
                Canvas.SetLeft(Schnittpunkt, xs - 0.5 * Schnittpunkt.Width);
                Canvas.SetTop(Schnittpunkt, ys - 0.5 * Schnittpunkt.Height);
                Schnittpunkt.Visibility = Visibility.Visible;
            }
            catch(Exception)
            {
                Schnittpunkt.Visibility = Visibility.Hidden;
            }
        }
    }
}

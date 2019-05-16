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

namespace Haushaltsbuch
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EinmaligeBuchung b1 = new EinmaligeBuchung(42, new DateTime(2019, 5, 2), "A");
            MonatlicheBuchung b2 = new MonatlicheBuchung(100, new DateTime(2010, 1, 1), new DateTime(2020, 1, 4), "B");
            Kontobuch k = new Kontobuch();
            k.FügeBuchungHinzu(b1);
            k.FügeBuchungHinzu(b2);
            decimal d1 = k.BerechneStandAm(new DateTime(2019, 4, 4));
            decimal d2 = k.BerechneStandAm(new DateTime(2019, 5, 5));
            decimal d3 = k.BerechneStandAm(new DateTime(2019, 6, 6));

            Buchungsposten b3 = b1; // Upcast

            if (b3 is MonatlicheBuchung)
            {
                MonatlicheBuchung b4 = (MonatlicheBuchung)b3; // Downcast
            }

            MonatlicheBuchung b5 = b3 as MonatlicheBuchung; // Downcast
            if (b5 != null)
            {
                //...
            }

            textblockAusgabe.Text = k.GibProtokoll();
        }
    }
}

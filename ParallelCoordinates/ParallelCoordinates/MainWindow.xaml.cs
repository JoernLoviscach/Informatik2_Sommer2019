using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ParallelCoordinates
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Daten von https://opendata.bonn.de/dataset/bu%C3%9Fgelder-flie%C3%9Fender-verkehr-2018

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                zeichenfläche.Children.Clear();

                // TODO: Exceptions behandeln!
                StreamReader sr = new StreamReader(ofd.FileName, Encoding.GetEncoding(850));

                // TODO: Auch der Lösungsansatz mit StreamGeometry skaliert noch nicht.
                // Man muss wohl Zwischenergebnisse in einer Bitmap sammeln.

                string zeile = null;
                StreamGeometry geometrie = null;
                System.Windows.Shapes.Path pfad = null;
                StreamGeometryContext ctx = null;
                int anzahl = 0;

                while ((zeile = sr.ReadLine()) != null)
                {
                    // Nicht zu viele Path-Objekte,
                    // aber auch nicht zu große Pfade.
                    if (anzahl % 50 == 0)
                    {
                        System.Diagnostics.Debug.WriteLine(anzahl);

                        if (anzahl != 0)
                        {
                            ctx.Close();
                            geometrie.Freeze();
                            pfad.Data = geometrie;
                            zeichenfläche.Children.Add(pfad);
                        }

                        if(anzahl > 150000) // Mehr will mein Notebook nicht machen.
                        {
                            break;
                        }

                        pfad = new System.Windows.Shapes.Path();
                        pfad.Stroke = Brushes.Blue;
                        pfad.StrokeThickness = 1.0;
                        pfad.Opacity = 0.01;

                        geometrie = new StreamGeometry();
                        ctx = geometrie.Open();
                    }
                    anzahl++;

                    try
                    {
                        string[] teile = zeile.Split(';');
                        string tatort = teile[2];
                        string[] teileTMJ = teile[0].Split('.');
                        DateTime tatzeit = new DateTime(
                            int.Parse(teileTMJ[2]),
                            int.Parse(teileTMJ[1]),
                            int.Parse(teileTMJ[0]),
                            int.Parse(teile[1].Substring(0, 2)),
                            int.Parse(teile[1].Substring(2, 2)),
                            0);
                        int tatbestand = int.Parse(teile[3]);
                        decimal geldbuße = decimal.Parse(teile[4]);

                        TimeSpan wannImJahr = tatzeit - new DateTime(2018, 1, 1);

                        ctx.BeginFigure(new Point(0, zeichenfläche.ActualHeight * (1.0 - Math.Floor(wannImJahr.TotalDays) / 364.0)), false, false);
                        ctx.LineTo(new Point(zeichenfläche.ActualWidth / 3, zeichenfläche.ActualHeight * (1.0 - (wannImJahr.TotalDays % 24.0) / 24.0)), true, false);
                        ctx.LineTo(new Point(zeichenfläche.ActualWidth * 2 / 3, zeichenfläche.ActualHeight * (1.0 - (tatort.ToUpper()[6] - 'A') / 25.0)), true, false);
                        ctx.LineTo(new Point(zeichenfläche.ActualWidth, zeichenfläche.ActualHeight * (double)(1 - geldbuße / 1760m)), true, false);
                    }
                    catch (Exception)
                    {
                        //TODO: Fehlerhafte Datensätze mitzählen!
                    }
                }

                sr.Close();

                if (!zeichenfläche.Children.Contains(pfad))
                {
                    ctx.Close();
                    geometrie.Freeze();
                    pfad.Data = geometrie;
                    zeichenfläche.Children.Add(pfad);
                }
            }
        }
    }
}

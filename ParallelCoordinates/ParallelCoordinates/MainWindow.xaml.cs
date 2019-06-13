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
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
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
            if(ofd.ShowDialog() == true)
            {
                zeichenfläche.Children.Clear();
                string[] zeilen = File.ReadAllLines(ofd.FileName, Encoding.GetEncoding(850));
                for (int i = 1; i < zeilen.Length; i += 10)
                {
                    try
                    {
                        string[] teile = zeilen[i].Split(';');
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

                        Polyline polyline = new Polyline();
                        polyline.Points.Add(new Point(0, zeichenfläche.ActualHeight * (1 - (tatzeit - new DateTime(2018, 1, 1)).TotalHours / 8760 )));
                        polyline.Points.Add(new Point(zeichenfläche.ActualWidth / 3, 0));
                        polyline.Points.Add(new Point(zeichenfläche.ActualWidth * 2 / 3, 0));
                        polyline.Points.Add(new Point(zeichenfläche.ActualWidth, zeichenfläche.ActualHeight * (double)(1 - geldbuße / 1760m)));
                        polyline.Stroke = Brushes.Blue;
                        polyline.Opacity = 0.01;
                        polyline.StrokeThickness = 1.0;
                        zeichenfläche.Children.Add(polyline);
                    }
                    catch (Exception)
                    {
                        //TODO: Mitzählen!
                    }
                }
            }
        }
    }
}

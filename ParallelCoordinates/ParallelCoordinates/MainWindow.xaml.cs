﻿using Microsoft.Win32;
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
                string[] zeilen = File.ReadAllLines(ofd.FileName, Encoding.GetEncoding(850));
                for (int i = 1; i < zeilen.Length; i++)
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
                    int geldbuße = int.Parse(teile[4]);

                }

            }
        }
    }
}
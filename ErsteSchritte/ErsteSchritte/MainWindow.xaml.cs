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

namespace ErsteSchritte
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
    }
}

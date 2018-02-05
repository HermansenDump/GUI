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
using Sailboat01.Models;

namespace Sailboat01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Sailboat _sb;

        public MainWindow()
        {
            InitializeComponent();
            _sb = new Sailboat();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(NameInputBox);
        }

        private void CalculateSpeed_OnClick(object sender, RoutedEventArgs e)
        {
            KnotOutputBox.Text = _sb.Hullspeed().ToString("F1");
        }

        private void NameInputBox_TextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            _sb.Name = NameInputBox.Text;
        }

        private void LengthInputBox_TextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (int.TryParse((string)LengthInputBox.Text, out var returnedValue))
            {
                _sb.Length = returnedValue;
            }
            else
            {
                MessageBox.Show($"{LengthInputBox} does not contain a valid number");
            }
        }
    }
}

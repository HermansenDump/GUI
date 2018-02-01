using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DeltagerListe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string[] tokens;
            char[] separators = { ';' };
            string curStr;
            List<string> listOfStrings = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\KaspersLaptop\Documents\GitHub\4thSemester\GUI\Lecture02\DeltagerListe\DeltagerListe\02 deltagerliste.csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        curStr = sr.ReadLine();
                        if (curStr != null)
                        {
                            tokens = curStr.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                            listOfStrings.Add($"{tokens[0]}\t{ tokens[1]}\t{ tokens[2]}\t{ tokens[3]}\t{ tokens[4]}\t");

                        }
                    }
                    
                }

                ListBox.ItemsSource = listOfStrings;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
    }
}

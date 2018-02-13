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
using BabyNames02.Models;
using BabyNames02.Utility;

namespace BabyNames02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<BabyName> _namesCollection;
        private string[,] _rankMatrix = new string[11,10];

        public MainWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
            LstDecade.SelectionChanged += new SelectionChangedEventHandler(LstDecade_SelectionChanged);
        }

        private void LstDecade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem item;

            item = LstDecade.SelectedItem as ListBoxItem;
            if (item != null)
            {
                int decade = (Convert.ToInt32(item.Content) - 1900) / 10;
                LstDecadeTopNames.Items.Clear();
                for (int i = 1; i < 11; ++i)
                {
                    LstDecadeTopNames.Items.Add($"{i,2} {_rankMatrix[decade, i - 1]}");
                }
            }
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string filename = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "babynames.txt");
            this._namesCollection = Utility.Utility.ReadBabyNames(filename);

            foreach (var name in _namesCollection)
            {
                for (int decade = 1900; decade < 2010; decade += 10)
                {
                    int rank = name.Rank(decade);
                    int decadeIndex = (decade - 1900) / 10;
                    if (0 < rank && rank < 11)
                    {
                        if (_rankMatrix[decadeIndex, rank - 1] == null)
                        {
                            _rankMatrix[decadeIndex, rank - 1] = name.Name;
                        }
                        else
                        {
                            _rankMatrix[decadeIndex, rank - 1] += " and " + name.Name;
                        }
                    }
                }
            }
        }
    }
}

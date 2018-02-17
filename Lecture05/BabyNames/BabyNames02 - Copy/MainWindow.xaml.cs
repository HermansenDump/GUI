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
using BabyNames03.Models;
using BabyNames03.Utility;

namespace BabyNames03
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
            BtnSearch.Click += new RoutedEventHandler(Search);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            string name = TextBoxName.Text;

            // search through collection

            int i;
            for (i = 0; i < _namesCollection.Count; ++i)
            {
                if (_namesCollection[i].Name == name)
                {
                    break;
                }
            }

            if (-1 < i && i < _namesCollection.Count)
            {
                TextBlockSearchError.Text = "";
                BabyName theName = _namesCollection[i];
                TextBoxAverage.Text = theName.AverageRank().ToString();

                if (theName.Trend() > 0)
                {
                    TextBoxTrend.Text = "More popular";
                }
                else if (theName.Trend() == 0)
                {
                    TextBoxTrend.Text = "Inconclusive";
                }
                else
                {
                    TextBoxTrend.Text = "Less popular";
                }

                NamesRanking.Items.Clear();
                for (int year = 1900; year < 2001; year += 10)
                {
                    int rank = theName.Rank(year);
                    NamesRanking.Items.Add($"{year:####}   {rank:####}");
                }
            }
            else
            {
                TextBlockSearchError.Text = "Name not found";
                TextBoxAverage.Text = "";
                TextBoxTrend.Text = "";
                NamesRanking.Items.Clear();
            }
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

        private void MenuItemExit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItemSmallFont_OnClick(object sender, RoutedEventArgs e)
        {
            FontSize = 8;
        }

        private void MenuItemNormalFont_OnClick(object sender, RoutedEventArgs e)
        {
            FontSize = 12;
        }

        private void MenuItemLargeFont_OnClick(object sender, RoutedEventArgs e)
        {
            FontSize = 18;
        }

        private void MenuItemVeryLargeFont_OnClick(object sender, RoutedEventArgs e)
        {
            FontSize = 40;
        }
    }
}

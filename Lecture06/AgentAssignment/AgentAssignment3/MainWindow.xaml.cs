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

namespace AgentAssignment3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LeftArrowButton.Click += new RoutedEventHandler(LeftArrowButton_OnClick);
            RightArrowButton.Click += new RoutedEventHandler(RightArrowButton_OnClick);
            AddNewButton.Click += new RoutedEventHandler(AddNewButton_OnClick);
        }

        private void LeftArrowButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (AgentsListBox.SelectedIndex > 0)
            {
                AgentsListBox.SelectedIndex--;
            }
        }

        private void RightArrowButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (AgentsListBox.SelectedIndex < AgentsListBox.Items.Count)
            {
                AgentsListBox.SelectedIndex++;
            }
        }

        private void AddNewButton_OnClick(object sender, RoutedEventArgs e)
        {
            Agents agents = (Agents) this.FindResource("Agents");
            agents.Add(new Agent());
            AgentsListBox.SelectedIndex = AgentsListBox.Items.Count - 1;
            IdTextBox.Focus();
        }
    }
}

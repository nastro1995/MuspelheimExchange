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

namespace MuspelheimExchange.Views
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        private MainWindow RootWindow { get; set; }

        public DashboardPage(MainWindow rootWindow)
        {
            InitializeComponent();
            RootWindow = rootWindow;
            Loaded += DashboardPage_Loaded;
        }

        private void DashboardPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_Navigate_Search_Btn_Click(object sender, RoutedEventArgs e)
        {
            RootWindow.Navigate(new SearchPage(RootWindow));
        }

        private void Menu_Navigate_Catalogues_Btn_Click(object sender, RoutedEventArgs e)
        {
            RootWindow.Navigate(new CataloguePage(RootWindow));
        }
    }
}

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
using MuspelheimExchange.Views;
using MuspelheimExchange.Windows;
using MuspelScape;
using MuspelScape.Models;
using Newtonsoft.Json;

namespace MuspelheimExchange
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        public void Navigate(object content, object data = null)
        {
            if (data == null)
            {
                Root_Frame.Navigate(content);
            }
            else
            {
                Root_Frame.Navigate(content, data);
            }
        }

        public void ToggleBarState(bool? isOpen)
        {
            if (isOpen == true)
            {
                Bar_ShowHide_Btn.Content = @"\/";
                Bar_Border.Visibility = Visibility.Collapsed;
                Bar_ShowHide_Btn.ToolTip = "Show Bar";
                Bar_ShowHide_Btn.IsChecked = true;
            }
            else
            {
                Bar_ShowHide_Btn.Content = @"/\";
                Bar_Border.Visibility = Visibility.Visible;
                Bar_ShowHide_Btn.ToolTip = "Hide Bar";
                Bar_ShowHide_Btn.IsChecked = false;
            }
        }

        public void OpenOptionsDialog()
        {
            OptionsWindow optionsW = new OptionsWindow(this);
            if (optionsW.ShowDialog() == true)
            {
                /* Todo:
                 * -create a few basic options
                 * -options load from dialog window
                 */
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Navigate(new DashboardPage(this));
        }

        private void Bar_Search_Btn_Click(object sender, RoutedEventArgs e)
        {
            Navigate(new SearchPage(this));
        }

        private void Bar_BrowseCata_Btn_Click(object sender, RoutedEventArgs e)
        {
            Navigate(new CataloguePage(this));
        }

        private void Bar_Home_Btn_Click(object sender, RoutedEventArgs e)
        {
            Navigate(new DashboardPage(this));
        }

        private void Bar_Settings_Options_Btn_Click(object sender, RoutedEventArgs e)
        {
            OpenOptionsDialog();
        }

        private void Bar_ShowHide_Btn_Click(object sender, RoutedEventArgs e)
        {
            ToggleBarState(Bar_ShowHide_Btn.IsChecked);
        }
    }
}

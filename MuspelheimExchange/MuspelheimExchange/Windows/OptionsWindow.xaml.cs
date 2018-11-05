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
using System.Windows.Shapes;

namespace MuspelheimExchange.Windows
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        private MainWindow RootWindow { get; set; }

        public OptionsWindow(MainWindow rootWindow)
        {
            InitializeComponent();
            RootWindow = rootWindow;
            if (RootWindow != null)
            {
                Owner = RootWindow;
            }
            Loaded += OptionsWindow_Loaded;
        }

        private void OptionsWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

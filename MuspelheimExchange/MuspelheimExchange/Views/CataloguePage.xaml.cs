using MuspelScape;
using MuspelScape.Models.Catalogue;
using MuspelScape.Models.Items;
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
    /// Interaction logic for CataloguePage.xaml
    /// </summary>
    public partial class CataloguePage : Page
    {
        private CatalogueView Catalogue { get; set; }
        private MainWindow RootWindow { get; set; }

        public CataloguePage(MainWindow rootWindow)
        {
            InitializeComponent();
            RootWindow = rootWindow;
            Loaded += CataloguePage_Loaded;
        }

        private void CataloguePage_Loaded(object sender, RoutedEventArgs e)
        {
            RootWindow.ToggleBarState(false);
        }

        private void ViewCatalogue_Raw(string category, string startChar, string pageNumber)
        {
            ClearDisplay();
            char? _startChar = null;
            if (startChar != "")
            {
                if (int.TryParse(category, out int _category))
                {
                    if (int.TryParse(pageNumber, out int _page))
                    {
                        if (startChar.Length == 1)
                        {
                            _startChar = Convert.ToChar(startChar);
                        }
                        else
                        {
                            _startChar = Convert.ToChar(startChar.Substring(0, 1));
                        }
                        Catalogue = GE.GetCatalogue(_category, (char)_startChar, _page);
                        if (Catalogue != null)
                        {
                            ResetDisplay(1);
                        }
                    }
                }
            }
        }

        private void ResetDisplay(int sortMode = 0)
        {
            if (sortMode >= 0 && sortMode <= 2)
            {
                if (sortMode == 0)
                {
                    Display.ItemsSource = Catalogue.Items;
                }
                else if (sortMode == 1)
                {
                    Display.ItemsSource = Catalogue.Items.OrderBy(i => i.Name);
                }
                else if (sortMode == 2)
                {
                    Display.ItemsSource = Catalogue.Items.OrderByDescending(i => i.Name);
                }
            }
        }

        private void ClearDisplay()
        {
            Display.ItemsSource = null;
        }

        private void Display_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Display.SelectedItem is Item item)
            {
                ItemWindow iw = new ItemWindow(item)
                {
                    Owner = RootWindow
                };
                iw.Show();
            }
        }

        private void GetCatalogue_Btn_Click(object sender, RoutedEventArgs e)
        {
            ViewCatalogue_Raw(Category_Input.Text, StartingChar_Input.Text, PageNum_Input.Text);
        }
    }
}

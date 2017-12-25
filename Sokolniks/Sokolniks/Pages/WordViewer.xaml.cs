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
using TestWordDoc;

namespace Sokolniks.Pages
{
    /// <summary>
    /// Логика взаимодействия для WordViewer.xaml
    /// </summary>
    public partial class WordViewer : Page
    {
        public WordViewer()
        {
            InitializeComponent();
        }
        public WordViewer(String link)
        {
            InitializeComponent();
            try
            {
                IDocProcessing doc = new DocLoader(link, true, rrr);
            }
            catch
            {
                try
                {
                    IDocProcessing doc = new DocOpener(link, true, rrr);
                }
                catch
                {
                    NavigationService?.GoBack();
                }
            }
        }
    }
}

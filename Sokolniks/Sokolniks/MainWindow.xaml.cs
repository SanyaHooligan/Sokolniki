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
using Sokolniks.Pages;
using System.Windows.Media.Animation;
using Sokolniks.Model;
using Sokolniks.Service;
using Contracts;

namespace Sokolniks
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TheFrame.ContentRendered += TheFrame_ContentRendered;
            inactive = new InactionUser(ComeBackMainPage);
            inactive.Start();
        }

        private void TheFrame_ContentRendered(object sender, EventArgs e)
        {
            Session.CurrentSession.IsLoading = false;
        }

        InactionUser inactive;

        public void ComeBackMainPage()
        {
            if(TheFrame.Content as StartPage ==null)
            {
                TheFrame.Content = new StartPage();
            }
            
        }
        private void TheFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            Session.CurrentSession.IsLoading = true;
            if (e.Content!=null && !(e.Content is StartPage))
            {
                Session.CurrentSession.IsStartPage = false;
            }
            else if(e.Content!=null)
            {
                Session.CurrentSession.IsStartPage = true;
            }
        }
        
    }
}

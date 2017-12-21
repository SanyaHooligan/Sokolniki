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
            inactive = new InactionUser(ComeBackMainPage);
            inactive.Start();
        }
        InactionUser inactive;
        bool keyboardState = false;

        public void ComeBackMainPage()
        {
            if(TheFrame.Content as StartPage ==null)
            {
                TheFrame.Content = new StartPage();
            }
            
        }
        private void TheFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if(e.Content!=null && e.Content.GetType() != typeof(StartPage))
            {
                Date.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                CurrentTime.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                WeatherDescription.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                TemperatureValue.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                LogoImage.Source = new BitmapImage(new Uri("Images/logo.png",UriKind.Relative));
                ScheduleText.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                PhoneNumberText.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
            else if(e.Content!=null)
            {
                Date.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                CurrentTime.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                WeatherDescription.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                TemperatureValue.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                LogoImage.Source = new BitmapImage(new Uri("Images/logo_white.png", UriKind.Relative));
                ScheduleText.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                PhoneNumberText.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
            //spiinerGrid.Visibility = Visibility.Visible;
        }

        private void TheFrame_ContentRendered(object sender, EventArgs e)
        {
        //    Page FrameContent = TheFrame.Content as Page;
        //    var textboxCollection = UIHelper.GetLogicalChildCollection<TextBox>(FrameContent);
        //    keyboardState = false;
        //    foreach (var item in textboxCollection)
        //    {
        //        item.GotFocus += ShowKeyboard;
        //        item.LostFocus += HideKeyboard;
        //    }
        //    //if (FrameContent is MapPage)
        //    //{
        //    //    Message.DataContext = FrameContent.DataContext;
        //    //}
        //    //if (FrameContent is InfoPage || FrameContent is WebPage)
        //    //{
        //    //    BindKeyBoard(FrameContent);
        //    //}
        //}
        //private void HideKeyboard(object sender, RoutedEventArgs e)
        //{
        //    DoubleAnimation animation = new DoubleAnimation(1920 - Keyboard.ActualHeight, 1920, new Duration(new TimeSpan(0, 0, 0, 0, 200)));
        //    Storyboard sb = new Storyboard();
        //    sb.Children.Add(animation);
        //    Storyboard.SetTargetProperty(sb, new PropertyPath(Grid.HeightProperty));
        //    Storyboard.SetTarget(sb, MainFrameGrid);
        //    sb.BeginAnimation(HeightProperty, animation);
        //    sb.Begin();
        //}

        //private void ShowKeyboard(object sender, RoutedEventArgs e)
        //{
        //    DoubleAnimation animation = new DoubleAnimation(1920, 1920 - Keyboard.ActualHeight, new Duration(new TimeSpan(0, 0, 0, 0, 200)));
        //    Storyboard sb = new Storyboard();
        //    sb.Children.Add(animation);
        //    Storyboard.SetTargetProperty(sb, new PropertyPath(Grid.HeightProperty));
        //    Storyboard.SetTarget(sb, MainFrameGrid);
        //    sb.BeginAnimation(HeightProperty, animation);
        //    sb.Begin();
        }
    }
}

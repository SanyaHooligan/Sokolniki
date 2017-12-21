using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Sokolniks.Commands;
using CefSharp;
using System.ComponentModel;

namespace Sokolniks.Pages
{
    /// <summary>
    /// Логика взаимодействия для WebPage.xaml
    /// </summary>
    public partial class WebPage : Page
    {
        private void CefBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading==false)
            {
                e
                    .Browser
                    .MainFrame
                    .ExecuteJavaScriptAsync(
                "var links = document.querySelectorAll('a[target=\"_blank\"]');" +
                "for (var i = 0; i < links.length; i++)" +
                "{" +
                    "links[i].removeAttribute('target');" +
                "}" +
                "var newlinks = document.querySelectorAll('a');" +
                "for (var i = 0; i < newlinks.length; i++)" +
                "{" +
                    "var target = newlinks[i].getAttribute('href');" +
                    "if (target.indexOf('sokolniki.com') == -1 || target.indexOf('mailto') != -1)" +
                    "{" +
                        "newlinks[i].removeAttribute('href');" +
                    "}" +
                "}"
                
                );
            }
        }

        public WebPage()
        {
            //Cef.Initialize(configureBrowser());
            InitializeComponent();
            this.Loaded += WebPage_Loaded;
            this.Unloaded += WebPage_Unloaded;
        }

        private void WebPage_Unloaded(object sender, RoutedEventArgs e)
        {
            CefBrowser.Dispose();
        }

        private void WebPage_Loaded(object sender, RoutedEventArgs e)
        {
            ThicknessAnimationUsingKeyFrames anim = new ThicknessAnimationUsingKeyFrames();
            anim.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 350));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 0, -(int)2160 * 0.59, 0), KeyTime.FromPercent(.11)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 0, -(int)2160 * 0.3, 0), KeyTime.FromPercent(.31)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 0, -(int)2160 * 0.11, 0), KeyTime.FromPercent(.56)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 0, -(int)2160 * 0.03, 0), KeyTime.FromPercent(.75)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 0, 0, 0), KeyTime.FromPercent(1)));
            DoubleAnimation opacutyAnim = new DoubleAnimation(1, new Duration(new TimeSpan(0, 0, 0, 0, 350)));
            MainGrid.BeginAnimation(MarginProperty, anim);
            MainGrid.BeginAnimation(OpacityProperty, opacutyAnim);
        }

        private ICommand _GoBackCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand GoBackCommand => _GoBackCommand ?? (_GoBackCommand = new Command(delegate
        {
            if (CefBrowser.CanGoBack && CefBrowser.Address!="http://park.sokolniki.com/events")
                CefBrowser.Back();
            else
            {
                ThicknessAnimation marginAnim = new ThicknessAnimation(new Thickness(0, 0, -2160, 0), new Duration(new TimeSpan(0, 0, 0, 0, 350)));
                DoubleAnimation opacityAnim = new DoubleAnimation(0, new Duration(new TimeSpan(0, 0, 0, 0, 350)));
                marginAnim.Completed += (s, e) =>
                {
                    NavigationService.Navigate(new StartPage());
                };
                MainGrid.BeginAnimation(OpacityProperty, opacityAnim);
                MainGrid.BeginAnimation(MarginProperty, marginAnim);
            }
        }));

        //private CefSettings configureBrowser()
        //{
        //    var settings = new CefSettings
        //    {
        //        BrowserSubprocessPath = "CefSharp.BrowserSubprocess.exe"
        //    };
        //    settings.CefCommandLineArgs["touch-events"] = "enabled";
        //    return settings;
        //}
    }
}

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
using Sokolniks.Commands;
using System.Windows.Media.Animation;
using System.Threading;
using System.Windows.Threading;

namespace Sokolniks.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
            this.Loaded += StartPage_Loaded;
        }

        private void StartPage_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.Opacity = 0;
            ThicknessAnimationUsingKeyFrames anim = new ThicknessAnimationUsingKeyFrames();
            anim.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 350));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(-(int)2160 * 0.59, 0, 0, 0), KeyTime.FromPercent(.11)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(-(int)2160 * 0.3, 0, 0, 0), KeyTime.FromPercent(.31)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(-(int)2160 * 0.11, 0, 0, 0), KeyTime.FromPercent(.56)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(-(int)2160 * 0.03, 0, 0, 0), KeyTime.FromPercent(.75)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 0, 0, 0), KeyTime.FromPercent(1)));
            DoubleAnimation opacutyAnim = new DoubleAnimation(1, new Duration(new TimeSpan(0, 0, 0, 0, 350)));
            MainGrid.BeginAnimation(MarginProperty, anim);
            MainGrid.BeginAnimation(OpacityProperty, opacutyAnim);
        }

        private ICommand _MapCommand;
        public ICommand MapCommand => _MapCommand ?? (_MapCommand = new Command(delegate
        {

            ThicknessAnimation marginAnim = new ThicknessAnimation(new Thickness(-2160, 0, 0, 0), new Duration(new TimeSpan(0, 0, 0, 0, 350)));
            DoubleAnimation opacityAnim = new DoubleAnimation(0, new Duration(new TimeSpan(0, 0, 0, 0, 350)));
            marginAnim.Completed += Anim_Completed_Map;
            MainGrid.BeginAnimation(OpacityProperty, opacityAnim);
            MainGrid.BeginAnimation(MarginProperty, marginAnim);
        }));

        private ICommand _WebCommand;
        public ICommand WebCommand => _WebCommand ?? (_WebCommand = new Command(delegate
        {

            ThicknessAnimation marginAnim = new ThicknessAnimation(new Thickness(-2160, 0, 0, 0), new Duration(new TimeSpan(0, 0, 0, 0, 350)));
            DoubleAnimation opacityAnim = new DoubleAnimation(0, new Duration(new TimeSpan(0, 0, 0, 0, 350)));
            marginAnim.Completed += Anim_Completed_Web;
            MainGrid.BeginAnimation(OpacityProperty, opacityAnim);
            MainGrid.BeginAnimation(MarginProperty, marginAnim);
        }));

        private void Anim_Completed_Map(object sender, EventArgs e)
        {
            NavigationService?.Navigate(new MapPage());
        }

        private void Anim_Completed_Web(object sender, EventArgs e)
        {
            NavigationService?.Navigate(new WebPage());
        }
    }

}

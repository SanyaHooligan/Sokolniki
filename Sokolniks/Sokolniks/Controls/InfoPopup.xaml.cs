using Sokolniks.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Sokolniks.Controls
{
    /// <summary>
    /// Логика взаимодействия для InfoPopup.xaml
    /// </summary>
    public partial class InfoPopup : UserControl
    {
        public InfoPopup()
        {
            InitializeComponent();
            this.Loaded += InfoPopup_Loaded;
        }

        private void InfoPopup_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= InfoPopup_Loaded;
            pg = GetDependencyObject(this, typeof(MapPage)) as MapPage;
        }

        private MapPage pg;
        public String code { get; set; }
        public string Displaytext
        {
            get { return (string)GetValue(DisplaytextProperty); }
            set { SetValue(DisplaytextProperty, value); }
        }

        public static readonly DependencyProperty DisplaytextProperty =
            DependencyProperty.Register("Displaytext", typeof(string), typeof(UserControl), new PropertyMetadata(String.Empty));

        public Visibility PopupVisibility
        {
            get { return (Visibility)GetValue(PopupVisibilityProperty); }
            set { SetValue(PopupVisibilityProperty, value); }
        }

        public static readonly DependencyProperty PopupVisibilityProperty =
            DependencyProperty.Register("PopupVisibility", typeof(Visibility), typeof(UserControl), new PropertyMetadata(Visibility.Hidden));


        public Brush IconFill
        {
            get { return (Brush)GetValue(IconFillProperty); }
            set { SetValue(IconFillProperty, value); }
        }

        public static readonly DependencyProperty IconFillProperty =
            DependencyProperty.Register("IconFill", typeof(Brush), typeof(UserControl), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(100,154,122,93))));



        public Uri IconSource
        {
            get { return (Uri)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(Uri), typeof(UserControl), new PropertyMetadata(new Uri("../Images/Icons/24.png", UriKind.Relative)));

        private DependencyObject
        GetDependencyObject(DependencyObject startObject, Type type)
        {
            DependencyObject parent = startObject;
            while (parent != null)
            {
                if (type.IsInstanceOfType(parent))
                {
                    break;
                }
                else
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
            }
            return parent;
        }
        
        public void ClosePopup(object sender, MouseEventArgs e)
        {
            this.SetValue(Canvas.ZIndexProperty, 4);
            PopupVisibility = Visibility.Hidden;
            IconFill = Brushes.Transparent;
        }

        public void ShowPopup(object sender, MouseEventArgs e)
        {
            this.SetValue(Canvas.ZIndexProperty, 100);
            //PopupVisibility = Visibility.Visible;
            IconFill = new SolidColorBrush(Color.FromArgb(100, 154, 122, 93));
            CreateRoute(null, null);
        }

        private void CreateRoute(object sender, MouseEventArgs e)
        {
            pg.DrawRoute("start", code);
            pg.HideAllPopups();
            PopupVisibility = Visibility.Visible;
            this.SetValue(Canvas.ZIndexProperty, 100);
            IconFill = new SolidColorBrush(Color.FromArgb(180, 154, 122, 93));
            //ShowPopup(null, null);
        }
    }
}

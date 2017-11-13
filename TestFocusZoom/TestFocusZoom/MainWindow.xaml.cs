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

namespace TestFocusZoom
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point _pointOnClick; // Click Position for panning
        private ScaleTransform _scaleTransform;
        private TranslateTransform _translateTransform;
        private TransformGroup _transformGroup;

        public MainWindow()
        {
            InitializeComponent();
            _translateTransform = new TranslateTransform();
            _scaleTransform = new ScaleTransform();
            _transformGroup = new TransformGroup();
            _transformGroup.Children.Add(_scaleTransform);
            _transformGroup.Children.Add(_translateTransform);

            MainCanvas.RenderTransform = _transformGroup;
        }

        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Capture Mouse
            MainCanvas.CaptureMouse();
            //Store click position relation to Parent of the canvas
            _pointOnClick = e.GetPosition((FrameworkElement)MainCanvas.Parent);
        }

        private void MainCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Release Mouse Capture
            MainCanvas.ReleaseMouseCapture();
            //Set cursor by default
            Mouse.OverrideCursor = null;
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            //ToolTip for informations
            showToolTip(e);
            //Return if mouse is not captured
            if (!MainCanvas.IsMouseCaptured) return;
            //Point on move from Parent
            Point pointOnMove = e.GetPosition((FrameworkElement)MainCanvas.Parent);
            //set TranslateTransform
            _translateTransform.X = MainCanvas.RenderTransform.Value.OffsetX - (_pointOnClick.X - pointOnMove.X);
            _translateTransform.Y = MainCanvas.RenderTransform.Value.OffsetY - (_pointOnClick.Y - pointOnMove.Y);
            //Update pointOnClic
            _pointOnClick = e.GetPosition((FrameworkElement)MainCanvas.Parent);
        }

        private void MainCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //Point de la souris
            Point mousePosition = e.GetPosition(MainCanvas);
            //Actual Zoom
            double zoomNow = Math.Round(MainCanvas.RenderTransform.Value.M11, 1);
            //ZoomScale
            double zoomScale = 0.1;
            //Positive or negative zoom
            double valZoom = e.Delta > 0 ? zoomScale : -zoomScale;
            //Point de la souris pour le panning et zoom/dezoom
            Point pointOnMove = e.GetPosition((FrameworkElement)MainCanvas.Parent);
            //RenderTransformOrigin (doesn't fully working)
            MainCanvas.RenderTransformOrigin = new Point(mousePosition.X / MainCanvas.ActualWidth, mousePosition.Y / MainCanvas.ActualHeight);
            //Appel du zoom
            Zoom(new Point(mousePosition.X, mousePosition.Y), zoomNow + valZoom);
        }

        /// Zoom function
        private void Zoom(Point point, double scale)
        {
            //Calcul des centres selon la position de la souris
            double centerX = (point.X - _translateTransform.X) / _scaleTransform.ScaleX;
            double centerY = (point.Y - _translateTransform.Y) / _scaleTransform.ScaleY;
            //Mise à l'échelle
            _scaleTransform.ScaleX = scale;
            _scaleTransform.ScaleY = scale;
            //Retablissement du translate pour éviter un décalage
            _translateTransform.X = point.X - centerX * _scaleTransform.ScaleX;
            _translateTransform.Y = point.Y - centerY * _scaleTransform.ScaleY;
        }

        /// Just For ToolTip
        private void showToolTip(MouseEventArgs e)
        {
            Point currentPos = e.GetPosition(MainCanvas);
            Point currentPos2 = e.GetPosition((FrameworkElement)MainCanvas.Parent);
            myToolTip.RenderTransform = new TranslateTransform(currentPos.X + 20, currentPos.Y);
            myToolTip.Text = "Cursor position from MainCanvas : X=" + currentPos.X + ";Y=" + currentPos.Y + "\n";
            myToolTip.Text += "Cursor position from Parent : X=" + currentPos2.X + ";Y=" + currentPos2.Y + "\n";
            myToolTip.Text += "OffsetXY of MainCanvas: X=" + MainCanvas.RenderTransform.Value.OffsetX + ";Y=" + MainCanvas.RenderTransform.Value.OffsetY + "\n";
            myToolTip.Text += "Size of MainCanvas : Width=" + MainCanvas.ActualWidth + ";Height=" + MainCanvas.ActualWidth + "\n";
            myToolTip.Text += "Size of Parent: Width=" + ((FrameworkElement)MainCanvas.Parent).ActualWidth + ";Height=" + ((FrameworkElement)MainCanvas.Parent).ActualHeight;
        }
    }
}

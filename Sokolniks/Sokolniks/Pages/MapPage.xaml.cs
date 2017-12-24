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
using System.Windows.Media.Effects;
using Contracts;
using System.Collections.ObjectModel;
using Sokolniks.ViewModels;
using Sokolniks.Controls;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sokolniks.Pages
{
    /// <summary>
    /// Логика взаимодействия для MapPage.xaml
    /// </summary>
    public partial class MapPage : Page
    {
        public MapPage()
        {
            InitializeComponent();
            CreateMarkCollection();
            popupcollection = canvasmap.Children.OfType<InfoPopup>();
            this.Loaded += MapPage_Loaded;
            if (Properties.Resources.FirstTimeOrNot == "0")
            {
                FindEllipses(true);
                SerializeMatrixes();
            }
            else
            {
                FindEllipses(false);
                DeserializeMatrixes();
            }
        }

        private void DeserializeMatrixes()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("matrix.xml", FileMode.Open))
            {
                matrix = (Double[,])formatter.Deserialize(fs);
            }
            formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("weightMatrix.xml", FileMode.Open))
            {
                weightMatrix = (Double[,])formatter.Deserialize(fs);
            }
            formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("pathMatrix.xml", FileMode.Open))
            {
                pathMatrix = (int[,])formatter.Deserialize(fs);
            }
        }

        private void SerializeMatrixes()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("matrix.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, matrix);
            }
            formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("weightMatrix.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, weightMatrix);
            }
            formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("pathMatrix.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, pathMatrix);
            }
        }

        public IEnumerable<InfoPopup> popupcollection;
        const double CANVASMAPHEIGHT = 1407;
        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Ellipse r = this.FindName(TestTextBox.Text) as Ellipse;
        //        if (r != null)
        //        {
        //            DrawRoute("start", TestTextBox.Text);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        private IEnumerable<Ellipse> ellipseCollection;
        private Polyline l;
        private const int INF = int.MaxValue;
        private int countCoordinates = 1;
        private Double[,] matrix;
        private Double[,] weightMatrix;
        private int[,] pathMatrix;
        private int length;
        private Point TempStartPoint;
        private Point TempEndPoint;

        private void MapPage_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dt = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.01) };
            dt.Tick += (s, ea) =>
             {
                 dt.Stop();
                 ThicknessAnimationUsingKeyFrames anim = new ThicknessAnimationUsingKeyFrames();
                 anim.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 1));
                 anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 0, -(int)2160 * 0.59, 0), KeyTime.FromPercent(.11)));
                 anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 0, -(int)2160 * 0.3, 0), KeyTime.FromPercent(.31)));
                 anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 0, -(int)2160 * 0.11, 0), KeyTime.FromPercent(.56)));
                 anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 0, -(int)2160 * 0.03, 0), KeyTime.FromPercent(.75)));
                 anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 0, 0, 0), KeyTime.FromPercent(1)));
                 DoubleAnimation opacutyAnim = new DoubleAnimation(1, new Duration(new TimeSpan(0, 0, 0, 0, 1)));
                 MainGrid.BeginAnimation(MarginProperty, anim);
                 MainGrid.BeginAnimation(OpacityProperty, opacutyAnim);
             };
            dt.Start();
        }

        private ICommand _GoBackCommand;
        public ICommand GoBackCommand => _GoBackCommand ?? (_GoBackCommand = new Command(delegate
         {
             ThicknessAnimation marginAnim = new ThicknessAnimation(new Thickness(0, 0, -2160, 0), new Duration(new TimeSpan(0, 0, 0, 0, 350)));
             DoubleAnimation opacityAnim = new DoubleAnimation(0, new Duration(new TimeSpan(0, 0, 0, 0, 350)));
             marginAnim.Completed += MarginAnim_Completed;
             MainGrid.BeginAnimation(OpacityProperty, opacityAnim);
             MainGrid.BeginAnimation(MarginProperty, marginAnim);
         }));

        private void MarginAnim_Completed(object sender, EventArgs e)
        {
            NavigationService?.Navigate(new StartPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThicknessAnimationUsingKeyFrames anim = new ThicknessAnimationUsingKeyFrames();
            anim.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 350));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 275 + 1645 * 0.59, 0, 0), KeyTime.FromPercent(.11)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 275 + 1645 * 0.3, 0, 0), KeyTime.FromPercent(.31)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 275 + 1645 * 0.11, 0, 0), KeyTime.FromPercent(.56)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 275 + 1645 * 0.03, 0, 0), KeyTime.FromPercent(.75)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 275, 0, 0), KeyTime.FromPercent(1)));
            SearchSpace.BeginAnimation(MarginProperty, anim);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ThicknessAnimationUsingKeyFrames anim = new ThicknessAnimationUsingKeyFrames();
            anim.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 350));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 275 + 1645 * 0.41, 0, 0), KeyTime.FromPercent(.11)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 275 + 1645 * 0.7, 0, 0), KeyTime.FromPercent(.31)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 275 + 1645 * 0.89, 0, 0), KeyTime.FromPercent(.56)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 275 + 1645 * 0.97, 0, 0), KeyTime.FromPercent(.75)));
            anim.KeyFrames.Add(new EasingThicknessKeyFrame(new Thickness(0, 1920, 0, 0), KeyTime.FromPercent(1)));
            SearchSpace.BeginAnimation(MarginProperty, anim);
        }
        #region Manipulation
        private void _canvas_ManipulationStarting(object sender,
                              ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = MainMapCanvas;
            e.Handled = true;
        }

        private void _canvas_ManipulationDelta(object sender,
                                 ManipulationDeltaEventArgs e)
        {
            HideAllPopups();
            var element = e.OriginalSource as UIElement;

            var transformation = element.RenderTransform
                                                 as MatrixTransform;
            var matrix1 = transformation == null ? Matrix.Identity :
                                           transformation.Matrix;

            //if (matrix1.M11 <= 3)
            matrix1.Translate(e.DeltaManipulation.Translation.X,
                             e.DeltaManipulation.Translation.Y);
            matrix1.ScaleAt(e.DeltaManipulation.Scale.X,
                           e.DeltaManipulation.Scale.Y,
                           e.ManipulationOrigin.X,
                           e.ManipulationOrigin.Y);
            //else if (matrix1.M11 > 3)
            //{
            //    matrix1.ScaleAt(3, 3, e.ManipulationOrigin.X, e.ManipulationOrigin.Y);
            //}
            //else if (matrix1.M11 <= 0.5)
            //{
            //    matrix1.ScaleAt(0.5, 0.5, e.ManipulationOrigin.X, e.ManipulationOrigin.Y);
            //}
            //else if (matrix1.M11 > 0.5 )
            //{
            //    matrix1.ScaleAt(e.DeltaManipulation.Scale.X,
            //                   e.DeltaManipulation.Scale.Y,
            //                   e.ManipulationOrigin.X,
            //                   e.ManipulationOrigin.Y);
            //}
            //else
            //{
            //    matrix1.ScaleAt(1, 1, e.ManipulationOrigin.X, e.ManipulationOrigin.Y);
            //}


            element.RenderTransform = new MatrixTransform(matrix1);
            if (matrix1.M11 > 1.5 || matrix1.M11 < 0.8)
            {
                element.RenderTransform = transformation;
            }
            if (matrix1.OffsetX > (1000 / matrix1.M11))
            {
                element.RenderTransform = transformation;
                //matrix1.OffsetX = (280 / matrix1.M11);
                //element.RenderTransform = new MatrixTransform(matrix1);
            }
            if (matrix1.OffsetY > (2000 / matrix1.M11))
            {
                element.RenderTransform = transformation;
                //matrix1.OffsetY = (280 / matrix1.M11);
                //element.RenderTransform = new MatrixTransform(matrix1);
            }
            if (matrix1.OffsetX < (-2800 * matrix1.M11))
            {
                element.RenderTransform = transformation;
                //matrix1.OffsetX = (-280 * matrix1.M11);
                //element.RenderTransform = new MatrixTransform(matrix1);
            }
            if (matrix1.OffsetY < (-300 * matrix1.M11))
            {
                element.RenderTransform = transformation;
                //matrix1.OffsetY = (-280 * matrix1.M11);
                //element.RenderTransform = new MatrixTransform(matrix1);
            }
            e.Handled = true;
        }
        #endregion
        #region HooliganMethods
        private Ellipse FindTag(int firstTag)
        {
            Ellipse outEllipse = null;
            foreach (var item in ellipseCollection)
            {
                if (item.Tag != null && item.Tag.ToString().Split(',')[0] == firstTag.ToString())
                {
                    outEllipse = item;
                }
            }
            if (outEllipse == null)
            {
                throw new NullReferenceException("Не найдено элемента с заданным Tag");
            }
            return outEllipse;
        }

        private void DrawTree()
        {

        }

        private int GetTagFirstElementFromEllipse(String ellipseName)
        {
            Ellipse ellipse;
            try
            {
                ellipse = this.FindName(ellipseName) as Ellipse;
            }
            catch (Exception ex)
            {
                ellipse = null;
                MessageBox.Show(ex.Message);
                throw new NullReferenceException("не надено точки");
            }
            String[] ellipseTag = ellipse.Tag.ToString().Split(',');
            int TagInt = 0;
            bool TagFlag = int.TryParse(ellipseTag[0], out TagInt);
            if (!TagFlag) throw new NullReferenceException("не удалось преобразовать элемент в int");
            return TagInt;
        }

        private void FindEllipses(bool isfirst)
        {
                ellipseCollection = canvasmap.Children.OfType<Ellipse>();
            if (!isfirst) return;
            matrix = new Double[ellipseCollection.Count(), ellipseCollection.Count()];
            length = matrix.GetLength(0);
            pathMatrix = new int[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (i == j) matrix[i, j] = 0;
                    else matrix[i, j] = INF;
                    pathMatrix[i, j] = j;
                }
            }
            foreach (var item in ellipseCollection)
            {
                List<String> tags = item.Tag.ToString().Split(',').ToList();
                int first = 0;
                bool flag = int.TryParse(tags[0], out first);
                if (!flag) throw new NullReferenceException("не удалось преобразовать первый элемент в int");
                //if (first == 6)
                //{

                //}
                tags.RemoveAt(0);
                foreach (var tag in tags)
                {
                    int TagInt = 0;
                    bool TagFlag = int.TryParse(tag, out TagInt);
                    if (!TagFlag) throw new NullReferenceException("не удалось преобразовать элемент в int");
                    matrix[first - 1, TagInt - 1] = GetLegthBetweenEllipses(FindTag(first), FindTag(TagInt));
                }
            }
            //for (int i = 0; i < length; i++)
            //{
            //    for (int j = 0; j < length; j++)
            //    {
            //        Debug.Write(matrix[i, j] + "        ");
            //    }
            //    Debug.WriteLine("");
            //}
            weightMatrix = matrix;
            //создание матрицы растояний между всеми элементами и матрицы предков
            for (int k = 0; k < length; k++)
            {
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        double min = weightMatrix[i, k] + weightMatrix[k, j];
                        if (weightMatrix[i, j] > min)
                        {
                            weightMatrix[i, j] = min;
                            pathMatrix[i, j] = pathMatrix[i, k];
                        }
                    }
                }
            }
        }

        private Double GetLegthBetweenPoints(Point start, Point end)
        {
            Double X = Math.Abs(end.X - start.X);
            Double Y = Math.Abs(end.Y - start.Y);
            Double legth = Math.Sqrt(X * X + Y * Y);
            return legth;
        }

        private Double GetLegthBetweenEllipses(Ellipse startEllipse, Ellipse endEllipse)
        {
            Point startPoint = new Point(Double.Parse(startEllipse.GetValue(Canvas.LeftProperty).ToString()), Double.Parse(startEllipse.GetValue(Canvas.TopProperty).ToString()));
            Point endPoint = new Point(Double.Parse(endEllipse.GetValue(Canvas.LeftProperty).ToString()), Double.Parse(endEllipse.GetValue(Canvas.TopProperty).ToString()));
            return GetLegthBetweenPoints(startPoint, endPoint);
        }

        //метод получения отдельного восстановленного пути
        private List<int> GetClosestRoute(int startIndex, int endIndex)
        {
            List<int> pathList = new List<int>();
            int k = startIndex - 1;
            while (k != endIndex - 1)
            {
                pathList.Add(k);
                k = pathMatrix[k, endIndex - 1];
            }
            pathList.Add(k);
            return pathList;
        }

        public void DrawRoute(String StartElement, String EndElement)
        {
            Ellipse startElement;
            Ellipse endElement;
            try
            {
                startElement = (Ellipse)this.FindName(StartElement);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("не найдено начальной точки");
            }

            try
            {
                endElement = (Ellipse)this.FindName(EndElement);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("не найдено конечной точки");
            }

            Point startPoint = new Point(Double.Parse(startElement.GetValue(Canvas.LeftProperty).ToString()), Double.Parse(startElement.GetValue(Canvas.TopProperty).ToString()));
            Point endPoint = new Point(Double.Parse(endElement.GetValue(Canvas.LeftProperty).ToString()), Double.Parse(endElement.GetValue(Canvas.TopProperty).ToString()));
            List<int> route = GetClosestRoute(GetTagFirstElementFromEllipse(StartElement), GetTagFirstElementFromEllipse(EndElement));
            if (l == null)
            {
                l = new Polyline();
                canvasmap.Children.Add(l);
            }
            l.Stroke = new SolidColorBrush(Color.FromRgb(255, 137, 173));
            l.StrokeThickness = 5.0;
            l.StrokeEndLineCap = PenLineCap.Round;
            l.StrokeStartLineCap = PenLineCap.Round;
            l.StrokeLineJoin = PenLineJoin.Round;
            l.Points = new PointCollection();
            l.Name = "NewLine";
            DropShadowEffect shadow = new DropShadowEffect();
            shadow.Opacity = 0.4;
            shadow.ShadowDepth = 5;
            shadow.Color = Colors.Black;
            shadow.Direction = 270;
            l.Effect = shadow;
            Ellipse startEllipse = FindTag(route[0] + 1);
            Point StartPoint = new Point(Double.Parse(startEllipse.GetValue(Canvas.LeftProperty).ToString()), Double.Parse(startEllipse.GetValue(Canvas.TopProperty).ToString()));
            l.Points.Add(new Point(StartPoint.X + startEllipse.ActualWidth / 2, StartPoint.Y + startEllipse.ActualHeight / 2));
            Canvas.SetZIndex(l, 1);
            if (route.Count < 3 && route[1] != 204)
            {
                throw new Exception("Не удается построить маршрут напрямую");
                //MessageBox.Show("Не удается построить маршрут к данной комнате\nВозможно, она находится в другом корпусе");
            }
            else
            {
                for (int i = 0; i < route.Count - 2; i++)
                {
                    DrawOneLine(route[i + 1]);
                }
            }
        }

        private void DrawOneLine(int EndElement)
        {
            Ellipse endEllipse = FindTag(EndElement + 1);
            TempEndPoint = new Point(Double.Parse(endEllipse.GetValue(Canvas.LeftProperty).ToString()), Double.Parse(endEllipse.GetValue(Canvas.TopProperty).ToString()));
            l.Points.Add(new Point(TempEndPoint.X + endEllipse.ActualWidth / 2, TempEndPoint.Y + endEllipse.ActualHeight / 2));
        }
        #endregion

        public void ShowOnePopup(MarkItem item)
        {
            //HideAllPopups();
            InfoPopup popup = FindPopup(item.MarkId);
            popup.SetValue(Canvas.ZIndexProperty, 100);
            popup.PopupVisibility = Visibility.Visible;
            popup.IconFill = new SolidColorBrush(Color.FromArgb(100, 154, 122, 93));
        }

        public void ShowOnePopup(MarkItem item, bool flag)
        {
            //HideAllPopups();
            InfoPopup popup = FindPopup(item.MarkId);
            popup.SetValue(Canvas.ZIndexProperty, 100);
            //popup.PopupVisibility = Visibility.Visible;
            popup.IconFill = new SolidColorBrush(Color.FromArgb(100, 154, 122, 93));
        }

        public void HideAllPopups()
        {
            foreach (var item in popupcollection)
            {
                item.PopupVisibility = Visibility.Hidden;
                item.IconFill = Brushes.Transparent;
                item.SetValue(Canvas.ZIndexProperty, 4);
            }
        }

        private ObservableCollection<MarkItem> items = new ObservableCollection<MarkItem>();
        private void CreateMarkCollection()
        {
            var viewmodel = (TestViewModelForSearch)this.DataContext;
            items = viewmodel.map.GetMarks();
            foreach (var item in items)
            {
                Ellipse el = this.FindName(item.MarkId) as Ellipse;
                if (el == null)
                {
                    throw new NullReferenceException("нету такой точки");
                }
                InfoPopup r = new InfoPopup() { code = item.MarkId, Displaytext = item.MarkName, IconSource = new Uri("../Images/Icons/" + item.SourceOfIcon, UriKind.Relative), IconFill = Brushes.Transparent };
                Point p = new Point(Double.Parse(el.GetValue(Canvas.LeftProperty).ToString()), Double.Parse(el.GetValue(Canvas.TopProperty).ToString()));
                r.SetValue(Canvas.LeftProperty, (p.X - 160));
                r.SetValue(Canvas.BottomProperty, (CANVASMAPHEIGHT - p.Y - 20));
                r.SetValue(Canvas.ZIndexProperty, 4);
                canvasmap.Children.Add(r);
            }
        }
        private InfoPopup FindPopup(String Code)
        {
            foreach (var item in popupcollection)
            {
                if (item.code == Code)
                {
                    return item;
                }
            }
            throw new ArgumentNullException("не найдено объекта");
        }
        private void ListMarks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListMarks.SelectedIndex != -1)
            {
                //HideAllPopups();
                MarkItem selectedItem = ListMarks.SelectedItem as MarkItem;
                Button_Click_1(null, null);
                HideAllPopups();
                ShowOnePopup(selectedItem);
                DrawRoute("start", selectedItem.MarkId);
            }
            ListMarks.SelectedIndex = -1;
        }

        private void Popular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PopularListBox.SelectedIndex != -1)
            {
                HideAllPopups();
                Сategory popular = PopularListBox.SelectedItem as Сategory;
                ShowCategory(popular);
            }
            PopularListBox.SelectedIndex = -1;
        }

        private void ListCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListCategories.SelectedIndex != -1)
            {
                HideAllPopups();
                Сategory selectedCategory = ListCategories.SelectedItem as Сategory;
                ShowCategory(selectedCategory);
            }
            ListCategories.SelectedIndex = -1;
        }

        private void FixedCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FixedCategoryListBox.SelectedIndex != -1)
            {
                HideAllPopups();
                Сategory popular = FixedCategoryListBox.SelectedItem as Сategory;
                ShowCategory(popular);
            }
            FixedCategoryListBox.SelectedIndex = -1;
        }

        private void ShowCategory(Сategory сategory)
        {
            Button_Click_1(null, null);
            if (l != null)
            {
                l.Points = new PointCollection();
            }
            foreach (var item in сategory.Marks)
            {
                try
                {
                    ShowOnePopup(item,true);
                }
                catch (ArgumentNullException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

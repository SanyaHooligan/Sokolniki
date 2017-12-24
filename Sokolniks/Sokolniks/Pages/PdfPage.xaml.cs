using MoonPdfLib.MuPdf;
using Sokolniks.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace Sokolniks.Pages
{
    /// <summary>
    /// Логика взаимодействия для PdfPage.xaml
    /// </summary>
    public partial class PdfPage : Page
    {
            public PdfPage()
            {
                InitializeComponent();
                this.Loaded += PdfViewer_Loaded;
            }
            public PdfPage(String uri)
            {
                //SpinnerVisibility = "Visible";
                Uri = uri;
                InitializeComponent();
                this.Loaded += PdfViewer_Loaded;
            }

            public String Uri
            {
                get { return (String)GetValue(UriProperty); }
                set { SetValue(UriProperty, value); }
            }

            // Using a DependencyProperty as the backing store for Uri.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty UriProperty =
                DependencyProperty.Register("Uri", typeof(String), typeof(UserControl), new PropertyMetadata(String.Empty));


            private void PdfViewer_Loaded(object sender, RoutedEventArgs e)
            {
                this.Loaded -= PdfViewer_Loaded;
                //PdfPanel.ZoomStep = 1.5;
                //Task newtask = Task.Factory.StartNew(() =>
                //{
                OpenPdf();
                //});
            }

            //public String SpinnerVisibility { get; set; }

            private void OpenPdf()
            {

                try
                {
                    PdfPanel.Open(new MemorySource(new WebClient().DownloadData(Uri)));
                }
                catch
                {
                    try
                    {
                        byte[] bytes = File.ReadAllBytes(Uri);
                        var source = new MemorySource(bytes);
                        PdfPanel.Open(source);
                    }
                    catch { }
                }
                //PdfPanel.PageRowDisplay = MoonPdfLib.PageRowDisplayType.ContinuousPageRows;
                //PdfPanel.ViewType = MoonPdfLib.ViewType.SinglePage;
                //PdfPanel.PageMargin = new Thickness(0, 10, 0, 10);
                PdfPanel.Zoom(1.5);
                //SpinnerVisibility = "Collapsed";
            }
        
    }
}

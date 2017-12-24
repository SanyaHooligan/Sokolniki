using Spire.Doc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Xps.Packaging;

namespace TestWordDoc
{
    public class DocOpener : IDocProcessing
    {
        public DocOpener(String filepath, bool startProcess, DocumentViewer viewer)
        {
            FilePath = filepath;
            if (startProcess)
            {
                OpenDoc(viewer);
            }
        }
        private String _FilePath;
        public String FilePath
        {
            get { return _FilePath; }
            set
            {
                _FilePath = value;
            }
        }
        private void ConvertToXPS()
        {
            string s = String.Empty;
            //List<String> StringList = FilePath.Split(new char[] { '.' }).ToList();
            //StringList.RemoveAt(StringList.Count - 1);
            //StringBuilder srt = new StringBuilder();
            //foreach(var item in StringList)
            //{
            //    srt.Append(item);
            //}
            Task t = Task.Run(() =>
            {
                Document doc = new Document();
                try
                {
                    doc.LoadFromFile(FilePath);
                }
                catch { }
                //s = srt.ToString() + ".xps";
                doc.SaveToFile("temp.xps", FileFormat.XPS);
            });
            t.Wait();

        }
        public void OpenDoc(DocumentViewer param)
        {
            ConvertToXPS();
            try
            {
                XpsDocument doc = new XpsDocument("temp.xps", FileAccess.Read, System.IO.Packaging.CompressionOption.Normal);
                param.Document = doc.GetFixedDocumentSequence();
            }
            catch(FileNotFoundException ex)
            {
                throw ex;
            }
        }
    }
}

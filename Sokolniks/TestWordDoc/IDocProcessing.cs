using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestWordDoc
{
    public interface IDocProcessing
    {
        String FilePath { get; set; }
        void OpenDoc(DocumentViewer param);
    }
}

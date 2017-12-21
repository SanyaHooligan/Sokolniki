
using Admin.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Admin.Model
{
  public  class Diserializer: IDiserializer
    {
        public List<Сategory> getCategories()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Сategory>));
            using (FileStream fs = new FileStream("../../../Сategories.xml", FileMode.Open))
            {
                return formatter.Deserialize(fs) as List<Сategory>;
            }
        }

        public List<MarkItem> getMarks()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<MarkItem>));
            using (FileStream fs = new FileStream("../../../Marks.xml", FileMode.Open))
            {
                return  formatter.Deserialize(fs) as List<MarkItem>;
            }
        }
    }
}

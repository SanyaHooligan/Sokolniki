
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Admin.Model
{
    class SerializerCol:ISerializer
    {
        public void Serializer(List<Сategory> list)
        {
           XmlSerializer formatter = new XmlSerializer(typeof(List<Сategory>));
            using (FileStream fs = new FileStream("../../../Сategories.xml", FileMode.Open))
            {
                formatter.Serialize(fs, list);
            }
        }
        public void Serializer(List<MarkItem> list)
        {
          XmlSerializer formatter = new XmlSerializer(typeof(List<MarkItem>));
            using (FileStream fs = new FileStream("../../../Marks.xml", FileMode.Open))
            {
                formatter.Serialize(fs, list);
            }
        }
        
    }
}

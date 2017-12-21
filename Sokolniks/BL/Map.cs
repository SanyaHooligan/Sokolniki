
using Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DownloadData;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace BL
{
      public  class Map
      {
        public List<MarkItem> Marks;
        public List<Сategory> Categories;
        private ICreateMarks createmark;
        public Map()
        {
            
            Marks = GetMarksFromDal();
            Categories = GetCategoriesFromDal();
        }
        private List<MarkItem> GetMarksFromDal()
            {
                createmark = new CreationMarks();
                List<MarkItem> list = createmark.CreateMarks();
            try
            {
                 if(list == null)
                    {
                        throw new NullReferenceException("Коллекция из DAl пустая");
                    }

                Environment.CurrentDirectory = Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location);
                XmlSerializer formatter = new XmlSerializer(typeof(List<MarkItem>));
                    using (FileStream fs = new FileStream("../../../../Marks.xml", FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, list);
                    }
            }
            catch(Exception)
            {

            }
            
            

            return list;
        }
        private List<Сategory> GetCategoriesFromDal()
        {
            createmark = new CreationMarks();
            List<Сategory> list = createmark.CreateCategory();
            if (list == null)
            {
                throw new NullReferenceException("Коллекция из DAl пустая");
            }

            Environment.CurrentDirectory = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);
            XmlSerializer formatter = new XmlSerializer(typeof(List<Сategory>));
            using (FileStream fs = new FileStream("../../../../Сategories.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, list);
            }
            return list;
        }
    }
}

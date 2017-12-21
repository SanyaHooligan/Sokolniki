using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model
{
    public class Search : ISearch,IEditData
    {

        List<Сategory> categoties;
        List<MarkItem> marks;
        Diserializer dis;
        ISerializer ser;

        public Search()
        {
            initCollextion();
            ser = new SerializerCol();
        }
        public void initCollextion()
        {
            dis = new Diserializer();
            categoties = dis.getCategories();
            marks = dis.getMarks();

        }
        public List<Сategory> getCategories()
        {
            return categoties;
        }

        public List<Сategory> getCategories(string text)
        {
            return categoties.Where(p => p.Name.ToLower().Contains(text.ToLower())).ToList();
        }

        public List<MarkItem> getMarks()
        {
            return marks;
        }

        public List<MarkItem> getMarks(string text)
        {
            return marks.Where(p => p.MarkName.ToLower().Contains(text.ToLower())).ToList();
        }

        public void editCategoty()
        {
//Сategory it = categoties.FirstOrDefault(p => p.type == item.type);
         //   it.Name = item.Name;
            ser.Serializer(categoties);
        }

        public void editMark()
        {
         //   MarkItem it = marks.FirstOrDefault(p => p.MarkId == item.MarkId);
           // it.MarkName= item.MarkName;
            ser.Serializer(marks);
        }

        public void editPicture()
        {
            ser.Serializer(marks);
        }
    }
}

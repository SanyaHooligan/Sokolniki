using Sokolniks.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using BL;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace Sokolniks.Model
{
   public class СategoriesMarks : ISearch
    {
        public Map map;
        public static List<Сategory> Categories;
        public СategoriesMarks()
        {
            map = new Map();
            Categories = map.Categories;
            ininCategory();
       
        }
        public void ininCategory()
        {
            for (int i = 0; i < Categories.Count; i++)
            {
                Categories[i].Marks = map.Marks.Where(p => p.Contains(Categories[i].type)).ToList();
            }
        }

        // public void addPriority(Сategory b )
        //{
        //    Catefories.First(t => t == b).AddPriority();
        //}
        //public bool isCategory(string text)
        //{
        //    if (Catefories.Where(p => p.Name.ToLower().Contains(text)) != null)
        //        return true;
        //    else return false;
        //}


        //public List<MarkType> getTypies(string text)
        //{
        //    return Catefories.Where(p => p.Name.ToLower().Contains(text.ToLower())).Select(p=>p.type).ToList();
        //}

        public ObservableCollection<MarkItem> GetMarks()
        {
            return new ObservableCollection<MarkItem>(map.Marks);
        }

        public ObservableCollection<Сategory> GetCategories()
        {
            return new ObservableCollection<Сategory>(Categories);
        }

        public ObservableCollection<Сategory> GetTopCategories()
        {
            return new ObservableCollection<Сategory>(Categories.OrderByDescending(p => p.priority).Take(7));
        }

        public ObservableCollection<Сategory> GetCategories(string text)
        {
            return new ObservableCollection<Сategory>(GetCategoriestt(text));
        }

        public ObservableCollection<MarkItem> GetMarks(string text)
        {
            List<MarkItem> listMark = map.Marks.Where(p => p.MarkName.ToLower().Contains(text.ToLower()) && p.access == true).ToList();

            List<MarkItem> listMarkresult = new List<MarkItem>();
            for (int i = 0; i < listMark.Count; i++)
            {
                MarkItem item = listMark[i];                
                if(listMark.Where(p=>p.MarkName==item.MarkName).ToList().Count > 1 )
                {
                    for (int j = 0; j < listMark.Count; j++)
                    {
                        if (listMark[j].MarkName == item.MarkName)
                        {
                            listMark.Remove(listMark[j]);
                            j--;
                        }
                    
                    }
                    i--;
                }
                else
                {
                    continue;
                }
            }
            return  new ObservableCollection < MarkItem > (listMark);
        }

        public List<Сategory> GetCategoriestt(string text)
        {
            List<Сategory> listCat = Categories.Where(p => p.Name.ToLower().Contains(text.ToLower())&p.Marks.Count>0).ToList();
            List<MarkItem> listMark = map.Marks.Where(p => p.MarkName.ToLower().Contains(text.ToLower()) && p.access == true).ToList();
            for (int i=0;i<listMark.Count; i++)
            {
                MarkItem item = listMark[i];
                Сategory t = new Сategory();
                for (int j = 0; j < listMark.Count; j++)
                {
                    if(item.MarkName== listMark[j].MarkName)
                    {
                        t.AddMark(listMark[j]);
                        listMark.Remove(listMark[j]);
                        j--;
                    }
                   
                }
                if(t.Marks.Count>1)
                {
                    listCat.Add(t);
                }

            }
            return listCat;

        }
    }
}

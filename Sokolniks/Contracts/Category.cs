using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [Serializable]
    public class Сategory
    {
        public MarkType type { get; set; }
        public int priority { get; set; }
        public string Name { get; set; }
        public List<MarkItem> Marks { get; set; }
        public int count
        {
            get
            {
                return Marks.Count;
            }
        }

        public Сategory(MarkType type, string name)
        {
            this.type = type;
            this.Name = name;
            priority = 0;
        }
        public Сategory(MarkType type, string name, bool access)
        {
            this.type = type;
            this.Name = name;
            priority = 0;
        }
        public Сategory()
        {
            priority = 0;
        }
        public void AddPriority()
        {
            priority++;
        }

        public void AddMark(MarkItem item)
        {
            if (Marks == null)
            {
                Marks = new List<MarkItem>();
                Name = item.MarkName;
            }
            Marks.Add(item);
        }

    }
}

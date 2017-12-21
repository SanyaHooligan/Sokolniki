using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model
{
    [Serializable]
    public class MarkItem
    {
        public String MarkName { get; set; }
        public String MarkId { get; set; }
        public List<MarkType> type { get; set; }
        public bool access { get; set; }
         public String SourceOfIcon { get; set; }
        public MarkItem(string markName, string markId, List<MarkType> marktype)
        {
            this.MarkName = markName;
            this.MarkId = markId;
            this.type = marktype;
            access = true;
        }
        public MarkItem(string markName, string markId, List<MarkType> marktype, bool access)
        {
            this.MarkName = markName;
            this.MarkId = markId;
            this.type = marktype;
            this.access = access;
        }
        public MarkItem()
        {

        }
        public bool Contein(MarkType type)
        {
            return this.type.Contains(type);
        }

    }
}

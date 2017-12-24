using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [Serializable]
    public class MarkItem
    {
        public String MarkName { get; set; }
        public String MarkId { get; set; }
        public String SourceOfIcon { get; set; }
        public List<MarkType> type { get; set; }
        public bool access { get; set; }
        public MarkItem(string markName,string markId,List<MarkType> marktype, String source)
        {
            this.MarkName = markName;
            this.MarkId = markId;
            this.type = marktype;
            this.SourceOfIcon = source;
            access = true;
        }
        public MarkItem(string markName, string markId, List<MarkType> marktype, String source, bool access)
        {
            this.MarkName = markName;
            this.MarkId = markId;
            this.type = marktype;
            this.SourceOfIcon = source;
            this.access = access;
        }
        public MarkItem()
        {
            
        }
        public bool Contains(MarkType type)
        {
            return this.type.Contains(type);
        }

    }
}

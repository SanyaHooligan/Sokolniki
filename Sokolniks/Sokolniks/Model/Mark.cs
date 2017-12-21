using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokolniks.Model
{
   public class Mark : MarkItem
    {
        public Mark(string markName, string markId, List<MarkType> marktype) : base(markName, markId, marktype)
        {
        }

        public bool Contein(MarkType type)
        {
            return this.type.Contains(type);
        }
    }
}

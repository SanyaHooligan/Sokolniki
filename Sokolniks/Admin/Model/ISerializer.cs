using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model
{
    interface ISerializer
    {
        void Serializer(List<Сategory> list);

        void Serializer(List<MarkItem> list);
       
    }
}

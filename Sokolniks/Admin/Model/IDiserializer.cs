using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model
{
    interface IDiserializer
    {

        List<Сategory> getCategories();
              
        List<MarkItem> getMarks();
      
    }
}

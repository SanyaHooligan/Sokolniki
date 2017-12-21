using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model
{
    interface ISearch
    {
        List<MarkItem> getMarks();
        List<MarkItem> getMarks(string text);
        List<Сategory> getCategories();
        List<Сategory> getCategories(string text);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICreateMarks
    {
        List<MarkItem> CreateMarks();

        List<Сategory> CreateCategory();
    }
}

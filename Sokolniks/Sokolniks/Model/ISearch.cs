using Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokolniks.Model
{
    public interface ISearch
    {
        ObservableCollection<MarkItem> GetMarks();
        ObservableCollection<Сategory> GetCategories();
        ObservableCollection<Сategory> GetTopCategories();

        ObservableCollection<Сategory> GetCategories(string text);
        ObservableCollection<MarkItem> GetMarks(string text);

    }
}

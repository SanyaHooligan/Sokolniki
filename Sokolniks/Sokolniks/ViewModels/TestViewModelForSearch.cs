using Contracts;
using Sokolniks.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokolniks.ViewModels
{
    enum Season { summer, winter, autumn }
    class TestViewModelForSearch : BaseViewModel
    {
        public ISearch map;
        public TestViewModelForSearch()
        {
            map = new СategoriesMarks();
            CheckSeason();
        }
        private ObservableCollection<MarkItem> marksForSearch;
        public ObservableCollection<MarkItem> MarksForSearch
        {
            get
            {
                return marksForSearch;
            }

            set
            {
                marksForSearch = value;
                DoPropertyChanged("MarksForSearch");
            }
        }


        private ObservableCollection<Сategory> categorisForSearch;
        public ObservableCollection<Сategory> CategorisForSearch
        {
            get
            {
                return categorisForSearch;
            }

            set
            {
                categorisForSearch = value;
                DoPropertyChanged("CategorisForSearch");
            }

        }


        private ObservableCollection<Сategory> categories;
        public ObservableCollection<Сategory> Categories
        {
            get
            {
                return categories;
            }

            set
            {
                categories = value;
                DoPropertyChanged("Categories");
            }
        }



        public ObservableCollection<Сategory> PopularCategorise
        {
            get
            {
                return map.GetTopCategories();
            }

        }

        public ObservableCollection<Сategory> AllCategories
        {
            get
            {
                return map.GetCategories();
            }

        }

        private string textSearch;
        public string TextSearch
        {
            get
            {
                return textSearch;
            }

            set
            {
                textSearch = value;
                if (string.IsNullOrWhiteSpace(textSearch))
                {
                    ClearCollections();
                }
                else
                {
                    CategorisForSearch = map.GetCategories(textSearch);
                    MarksForSearch = map.GetMarks(textSearch);
                }

                DoPropertyChanged("TextSearch");
            }
        }

        private String _MapImageSource;
        public String MapImageSource
        {
            get { return _MapImageSource; }
            set
            {
                _MapImageSource = value;
                CheckSeason();
                DoPropertyChanged("MapImageSource");
            }
        }
        private void ToggleSeason(Season currentSeason)
        {
            switch (currentSeason)
            {
                case Season.summer:
                    {
                        _MapImageSource = "../Images/Maps/Sokolniki_map_summer.png";
                    }
                    break;
                case Season.winter:
                    {
                        _MapImageSource = "../Images/Maps/Sokolniki_map_winter.jpg";
                    }
                    break;
                case Season.autumn:
                    {
                        _MapImageSource = "../Images/Maps/Sokolniki_map_autumn.jpg";
                    }
                    break;
                default:
                    {
                        _MapImageSource = "../Images/Maps/Sokolniki_map_summer.jpg";
                    }
                    break;
            }
        }

        private void CheckSeason()
        {
            DateTime date = DateTime.Now;
            if ((date.Month == 4 && date.Day >= 15) || (date.Month >= 5 && date.Month <= 8))
            {
                ToggleSeason(Season.summer);
            }
            else if(date.Month >= 9 && date.Month <= 11)
            {
                ToggleSeason(Season.autumn);
            }
            else
            {
                ToggleSeason(Season.winter);
            }
        }

        private void ClearCollections()
        {
            CategorisForSearch?.Clear();
            MarksForSearch?.Clear();
        }
        
    }
}

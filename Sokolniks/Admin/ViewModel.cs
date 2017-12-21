using Admin.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    public class ViewModel : INotifyPropertyChanged
    {
      public  ObservableCollection<string> Pictures { get; set; }
        public ViewModel()
        {
            search = new Search();
            Categories = new ObservableCollection<Сategory>( search.getCategories());
            Pictures = new ObservableCollection<string> { "WC.png", "mark.png", "parking.png", "case.png", "kacheli.png", "rabbit.png", "creditcard.png", "sport.png", "star.png", "hernya.png", "chainik.png", "shavuha.png", "turtle.png", "house.png", "photo.png", "vok.png", "list.png", "nature.png", "WC.png", "chair.png", "time.png", "cheburek.png", "roll.png", "tent.png", "krevetki.png", "disabled.png", "check.png", "info.png" };

        }
        Search search;



        private Сategory selectedCategory;
        public Сategory SelectedCategory
        {
            get
            {
                return selectedCategory;
            }

            set
            {
                selectedCategory = value;
                TextForCategory = selectedCategory.Name;
                Marks = new ObservableCollection<MarkItem> (search.getMarks().Where(p=>p.type.Contains(selectedCategory.type)));
                DoPropertyChanged("SelectedCategory");
            }
        }

      

        private MarkItem selectedMark;       
        public MarkItem SelectedMark
        {
            get
            {
                return selectedMark;
            }

            set
            {
                selectedMark = value;
                TextForMark = selectedMark.MarkName;
                DoPropertyChanged("SelectedMark");
            }
        }


        private string textSearchForCategories;
        public string TextSearchForCategories
        {
            get
            {
                return textSearchForCategories;
            }

            set
            {
                textSearchForCategories = value;
                if (!string.IsNullOrWhiteSpace(textSearchForCategories))
                {
                    Categories = new ObservableCollection<Сategory>(search.getCategories(textSearchForCategories));
                }
                else
                {
                    Categories = new ObservableCollection<Сategory>(search.getCategories());
                }
                DoPropertyChanged("TextSearchForCategories");
            }
        }

        private string textSearchForMarks;
        public string TextSearchForMarks
        {
            get
            {
                return textSearchForMarks;
            }

            set
            {
                textSearchForMarks = value;
                if (!string.IsNullOrWhiteSpace(textSearchForMarks))
                {
                    Marks = new ObservableCollection<MarkItem>(search.getMarks(textSearchForMarks));
                }
                else
                {
                    Marks = new ObservableCollection<MarkItem>(search.getMarks());
                }
                DoPropertyChanged("TextSearchForMarks");
            }
        }




        private string textForCategory;
        public string TextForCategory
        {
            get
            {
                return textForCategory;
            }

            set
            {
                textForCategory = value;
               
                DoPropertyChanged("TextForCategory");
            }
        }



        private string textForMark;
        public string TextForMark
        {
            get
            {
                return textForMark;
            }

            set
            {

                textForMark = value;
                DoPropertyChanged("TextForMark");
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

      
        private ObservableCollection<MarkItem> marks;
        public ObservableCollection<MarkItem> Marks
        {
            get
            {
                return marks;
            }

            set
            {
                marks = value;
                DoPropertyChanged("Marks");
            }
        }


        private string selectedPicture;
        public string SelectedPicture
        {
            get
            {
                return selectedPicture;
            }

            set
            {
                selectedPicture = value;
                DoPropertyChanged("SelectedPicture");
            }
        }

        private RelayCommand savePicture;
        public RelayCommand SavePicture
        {
            get
            {
                return savePicture ??
                  (savePicture = new RelayCommand(obj =>
                  {
                      selectedMark.SourceOfIcon = SelectedPicture;
                      search.editPicture();
                  }));
            }
        }
        private RelayCommand saveMark;
        public RelayCommand SaveMark
        {
            get
            {
                return saveMark ??
                  (saveMark = new RelayCommand(obj =>
                  {
                      SelectedMark.MarkName = textForMark;
                      search.editMark();
                  }));
            }
        }
        private RelayCommand saveCategory;
        public RelayCommand SaveCategory
        {
            get
            {
                return saveCategory ??
                  (saveCategory = new RelayCommand(obj =>
                  {
                      SelectedCategory.Name = textForCategory;
                      search.editCategoty();
                  }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void DoPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

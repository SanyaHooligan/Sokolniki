using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class Session:INotifyPropertyChanged
    {
        private static Session _currentSession;
        public static Session CurrentSession => _currentSession ?? (_currentSession = new Session());

        private bool _isStartPage = true;
        public bool IsStartPage
        {
            get { return _isStartPage; }
            set
            {
                _isStartPage = value;
                DoPropertyChanged("IsStartPage");
            }
        }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                DoPropertyChanged("IsLoading");
            }
        }

        #region PropertyChange
        public event PropertyChangedEventHandler PropertyChanged;
        public void DoPropertyChanged(String PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}

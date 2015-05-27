using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Vab.Domain;
using Vab.Helpers;

namespace Vab.ViewModels
{
    public class AddEditViewModel : INotifyPropertyChanged
    {

        #region Properties

        private ObservableCollection<Child> _children;
        public ObservableCollection<Child> Children
        {
            get { return _children; } 
            set 
            { 
                _children = value; 
                NotifyPropertyChanged("Children"); 
            }
        }

        #endregion

        public AddEditViewModel()
        {
            Load();
        }

        public void Load()
        {
            Children = IsolatedStorageHelper.Load();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
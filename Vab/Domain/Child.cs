using System;
using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using Vab.Helpers;

namespace Vab.Domain
{
    public class Child
    {

        #region Properties

        public string Guid { get; set; }

        private string _parentIdNumber;
        public string ParentIdNumber
        {
            get { return _parentIdNumber; } 
            set 
            {
                _parentIdNumber = value; 
                NotifyPropertyChanged("ParentIdNumber"); 
            }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                NotifyPropertyChanged("ImagePath");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private string _childIdNumber;
        public string ChildIdNumber
        {
            get { return _childIdNumber; }
            set
            {
                _childIdNumber = value;
                NotifyPropertyChanged("ChildIdNumber");
            }
        }

        #endregion

        #region Public methods

        public BitmapImage SavedImage
        {
            get
            {
                return GetSavedImage(ImagePath);
            }
        }

        public Child GetCopy()
        {
            return (Child)MemberwiseClone();
        }

        #endregion

        #region Private methods

        private static BitmapImage GetSavedImage(string filename)
        {
            try
            {
                BitmapImage savedImage;
                if (filename != Globals.DefaultChildIcon)
                {
                    using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (var isoStream = store.OpenFile(filename, FileMode.Open))
                        {
                            savedImage = new BitmapImage();
                            savedImage.SetSource(isoStream);
                        }
                    }
                }
                else
                {
                    savedImage = new BitmapImage(new Uri(filename, UriKind.Relative));
                }
                return savedImage;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        // Used to notify the page that a data context property changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}
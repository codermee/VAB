using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Tasks;
using Vab.Domain;
using Vab.Helpers;
using Vab.ViewModels;

namespace Vab.Views
{
    public partial class AddEditView
    {

        #region Members

        private string _imageFile = Globals.DefaultChildIcon;
        private static AddEditViewModel ViewModel { get; set; }
        private Child CurrentItem { get; set; }

        #endregion

        public AddEditView()
        {
            InitializeComponent();
            ViewModel = new AddEditViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var guid = GetQuerystring();

            if (!String.IsNullOrWhiteSpace(guid))
            {
                CurrentItem = ViewModel.Children.FirstOrDefault(x => x.Guid == guid);
	            LayoutRoot.DataContext = CurrentItem;
                PageTitle.Text = "redigera";
                if (CurrentItem != null && CurrentItem.SavedImage != null)
                {
                    ItemImage.Source = CurrentItem.SavedImage;
                    _imageFile = CurrentItem.ImagePath;
                }
            }
        }

        #region Private methods

        private string GetQuerystring()
        {
            string querystring;
            NavigationContext.QueryString.TryGetValue("guid", out querystring);
            NavigationContext.QueryString.Clear();
            return querystring;
        }

        #endregion

        #region Events

        private void OnPhotoPickerButtonClick(object sender, RoutedEventArgs e)
        {
            var photoChooserTask = new PhotoChooserTask
                    {
                        PixelHeight = 130,
                        PixelWidth = 130,
                    };
            photoChooserTask.Completed += OnPhotoChooserTaskCompleted;
            photoChooserTask.Show();
        }

        private void OnPhotoChooserTaskCompleted(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                var separator = new[] { "\\" };
                var filePathArray = e.OriginalFileName.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                var image = new BitmapImage();
                image.SetSource(e.ChosenPhoto);

                // Set image´s source on page
                ItemImage.Source = image;

                // Store the file name (isolated storage) to save to Child object on save
                _imageFile = Globals.ShellContentPath + filePathArray[filePathArray.Length - 1];

                // Save chosen image to isolated storage
                IsolatedStorageHelper.SaveImage(_imageFile, image);
            }
        }

        private void OnAppBarSaveIconClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(ChildIdNumber.Text) && !String.IsNullOrWhiteSpace(ChildName.Text) && !String.IsNullOrWhiteSpace(ParentIdNumber.Text))
            {
                if (CurrentItem != null)
                {
                    CurrentItem.ChildIdNumber = ChildIdNumber.Text;
                    CurrentItem.Name = ChildName.Text;
                    CurrentItem.ParentIdNumber = ParentIdNumber.Text;
                    CurrentItem.ImagePath = _imageFile;
                    IsolatedStorageHelper.Save(CurrentItem);
                }
                else
                {
                    var newChild = new Child
                        {
                            Guid = Convert.ToString(Guid.NewGuid()),
                            ChildIdNumber = ChildIdNumber.Text,
                            Name = ChildName.Text,
                            ParentIdNumber = ParentIdNumber.Text,
                            ImagePath = _imageFile
                        };
                    IsolatedStorageHelper.Save(newChild);
                }
                Helper.NavigateToUrl(Globals.MainUri);
            }
            else
            {
                MessageBox.Show(Globals.ErrorMessageAdd, Globals.ErrorMessageCaptionAdd, MessageBoxButton.OK);
            }
        }

        private void OnAppBarCancelIconClick(object sender, EventArgs e)
        {
            Helper.NavigateToUrl(Globals.MainUri);
        }

        #endregion
    }
}
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Vab.Domain;
using Vab.Helpers;
using Vab.ViewModels;

namespace Vab
{
    public partial class MainPage
    {

        #region Members

        public Child SelectedItem { get; set; }
        private static MainViewModel ViewModel { get; set; }

        #endregion

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
        }

        #region Events

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
	        base.OnNavigatedTo(e);
            while (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }

            if (ViewModel.Children != null)
            {
                ChildrenListBox.DataContext = ViewModel.Children;
            }
        }

        private void OnAppBarAddIconClick(object sender, EventArgs e)
        {
            Helper.NavigateToUrl(Globals.AddEditUri);
        }

        private void OnAppBarAboutIconClick(object sender, EventArgs e)
        {
            Helper.NavigateToUrl(Globals.AboutUri);
        }

        private void GestureListener_Hold(object sender, GestureEventArgs e)
        {
            // sender is the StackPanel in this example 
            var item = ((Grid)sender).DataContext;

            // item has the type of the model
            SelectedItem = item as Child;
        }

        private void OnChildrenListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Reset selected index to -1 (no selection)
            ChildrenListBox.SelectedIndex = -1;
        }

        private void OnContextMenuReportVabClick(object sender, RoutedEventArgs e)
        {
            var smsComposeTask = new SmsComposeTask
                {
                    To = Globals.SmsTo, 
                    Body = String.Format(Globals.SmsBody, SelectedItem.ParentIdNumber, SelectedItem.ChildIdNumber)
                };

            smsComposeTask.Show();
        }

        private void OnContextMenuEditClick(object sender, RoutedEventArgs e)
        {
            var uri = String.Format(Globals.AddEditWithQueryUri, SelectedItem.Guid);
            Helper.NavigateToUrl(uri);
        }

        private void OnContextMenuDeleteClick(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {
                var item = (from i in ViewModel.Children where i.Guid == SelectedItem.Guid select i).FirstOrDefault();

                var result = MessageBox.Show(String.Format(Globals.ConfirmationMessageDelete, SelectedItem.Name), Globals.ConfirmationMessageCaptionDelete, MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    IsolatedStorageHelper.Delete(item);
                    ViewModel.Load();
                    ChildrenListBox.DataContext = ViewModel.Children;
                }
            }
            else
            {
                MessageBox.Show(Globals.ErrorMessageDelete);
            }
        }

        #endregion

    }
}
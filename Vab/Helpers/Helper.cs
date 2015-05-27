using System;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;

namespace Vab.Helpers
{
    public class Helper
    {
        public static void NavigateToUrl(string url)
        {
            var uri = new Uri(url, UriKind.Relative);
            ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(uri);
        }
    }
}
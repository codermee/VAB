using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using Vab.Domain;

namespace Vab.Helpers
{
    public static class IsolatedStorageHelper
    {
        public static ObservableCollection<Child> Load()
        {
            var children = new ObservableCollection<Child>();
            if (IsolatedStorageSettings.ApplicationSettings.Count > 0)
            {
                foreach (var item in IsolatedStorageSettings.ApplicationSettings)
                {
                    children.Add((Child)item.Value);
                }
            }
            return children;
        }

        public static void Save(Child item)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;

            if (settings.Contains(item.Guid))
            {
                settings[item.Guid] = item;
            }
            else
            {
                settings.Add(item.Guid, item.GetCopy());
            }

            settings.Save();
        }

        public static void Delete(Child item)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(item.Guid))
            {
                settings.Remove(item.Guid);
            }
            settings.Save();
        }

        public static void SaveImage(string imageFile, BitmapImage bitmap)
        {
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (storage.FileExists(imageFile))
                {
                    storage.DeleteFile(imageFile);
                }

                var fileStream = storage.CreateFile(imageFile);
                var wb = new WriteableBitmap(bitmap);

                wb.Invalidate();
                wb.SaveJpeg(fileStream, wb.PixelWidth, wb.PixelHeight, 0, 100);
                fileStream.Close();
            }
        }
    }
}
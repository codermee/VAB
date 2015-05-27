using System;

namespace Vab.Helpers
{
    public class Globals
    {
        public static string MainUri = "/MainPage.xaml";
        public static string AboutUri = "/Views/AboutView.xaml";
        public static string AddEditUri = "/Views/AddEditView.xaml";
        public static string AddEditWithQueryUri = "/Views/AddEditView.xaml?guid={0}";
        public static string DetailsUri = "/Views/DetailsView.xaml?guid={0}";
        public static string ShellContentPath = "/Shared/ShellContent/";
        public static string DefaultChildIcon = "../Assets/child.png";
        public static string ErrorMessageAdd = "Du måste ange namn och personnummer på barnet samt vårdnadshavarens personnummer.";
        public static string ErrorMessageCaptionAdd = "Fel";
        public static string ErrorMessageDelete = "Oops, nåt gick fel!";
        public static string ConfirmationMessageDelete = "Vill du ta bort {0} från listan?";
        public static string ConfirmationMessageCaptionDelete = "Radera";
        public static string SmsBody = "TFP {0} {1}";
        public static string SmsTo = "71020";
    }
}
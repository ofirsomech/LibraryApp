using Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.Tools
{
   public class NavigateTool
    {
        public static void Nav(Page page)
        {
            var wind = Application.Current.MainWindow;
            var frame = (Frame)wind.Content;
            frame.Navigate(page);
        }

        public static void NavFromLogin()
        {
            Application.Current.MainWindow.Content = new Frame();

            NavigateTool.Nav(new MainLibraryPage());
        }

        public static void DisconectedNav()
        {
            Application.Current.MainWindow.Content = new LoginView();
        }
        public static void Close()
        {
            NavigateTool.Nav(new MainLibraryPage());
        }
    }
}

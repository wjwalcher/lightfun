using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LightFun.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LightFun
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(Dashboard));
            // TODO: Prompt user on start up to sign in
            // Don't navigate to dashboard until they do.
            App.user = new User("William"); // Stubbed
        }

        private void MainNavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem navItemSelected = (NavigationViewItem)args.SelectedItem;
            string selectedTabName = (string)navItemSelected.Tag;

            switch (selectedTabName)
            {
                case "Dashboard":
                    ContentFrame.Navigate(typeof(Dashboard));
                    break;
                case "Scenes":
                    ContentFrame.Navigate(typeof(Scenes));
                    break;
                case "Schedules":
                    ContentFrame.Navigate(typeof(Schedules));
                    break;
                case "Weather":
                    ContentFrame.Navigate(typeof(Weather));
                    break;
            }
        }

        private void MainNavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                PageStackEntry pse = ContentFrame.BackStack[ContentFrame.BackStack.Count - 1];
                var lastPageName = pse.SourcePageType.Name;
                switch(lastPageName)
                {
                    case "Dashboard":
                        MainNavView.SelectedItem = MainNavView.MenuItems.ElementAt(0);
                        break;
                    case "Scenes":
                        MainNavView.SelectedItem = MainNavView.MenuItems.ElementAt(1);
                        break;
                    case "Schedules":
                        MainNavView.SelectedItem = MainNavView.MenuItems.ElementAt(2);
                        break;
                    case "Weather":
                        MainNavView.SelectedItem = MainNavView.MenuItems.ElementAt(3);
                        break;
                }
            }
        }
    }
}

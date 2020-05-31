using LightFun.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightFun
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dashboard : Page
    {

        ObservableCollection<ColorItem> availableColors;
        ObservableCollection<LightItem> availableLights;

        public string GreetingText { 
            get
            {
                if (App.user != null)
                return "Hello, " + App.user.Name + "!";
                else
                {
                    System.Diagnostics.Debug.Fail("User instance not set up.");
                    return null;
                }
            } 
        }
        public Dashboard()
        {
            this.InitializeComponent();
            availableColors = AppInfoSingleton.GetInstance().GetColorItems();
            availableLights = AppInfoSingleton.GetInstance().GetLightItems();
        }
        
  

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                byte red = (byte)RedSlider.Value;
                byte green = (byte)GreenSlider.Value;
                byte blue = (byte)BlueSlider.Value;
               
                availableColors.Add(new ColorItem(new ColorDTO(red, green, blue)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            
        }

        private void ColorPickerGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorItem newSelection = (ColorItem)ColorPickerGridView.SelectedItem;

        }


        private void Button_Click_Set_Color(object sender, RoutedEventArgs e)
        {
            if (ColorPickerGridView.SelectedItem != null)
            {
                ColorDTO selectedColor = ((ColorItem)(ColorPickerGridView.SelectedItem)).color;
                HueCommunicationService hcs = new HueCommunicationService();
              
                foreach (LightItem lightItem in LightPickerGridView.SelectedItems)
                {
                    hcs.ChangeLightColor(selectedColor, lightItem.LightNumber);
                }
            }
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            ColorItem newSelection = (ColorItem)ColorPickerGridView.SelectedItem;
            availableColors.Remove(newSelection);

        }
    }
}

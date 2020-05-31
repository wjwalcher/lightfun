using LightFun.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class Scenes : Page
    {
        ObservableCollection<SceneItem> availableScenes;
        ObservableCollection<ColorItem> availableColors;
        ObservableCollection<LightItem> availableLights;
        ObservableCollection<SceneConfigItem> currentSceneConfig;

        public Scenes()
        {
            this.InitializeComponent();
            AppInfoSingleton singleton = AppInfoSingleton.GetInstance();
            availableScenes = singleton.GetSceneItems();
            availableColors = singleton.GetColorItems();
            availableLights = singleton.GetLightItems();
            currentSceneConfig = new ObservableCollection<SceneConfigItem>();
        }

        private void ColorPickerGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void ScenesPickerGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SceneItem selectedScene = (SceneItem)ScenesPickerGridView.SelectedItem;
            selectedScene.DoScene();
        }

        private void LightPickerGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SceneConfigGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Commit_Scene(object sender, RoutedEventArgs e)
        {
            string sceneName = SceneNameTextBox.Text;
            string ImageSource = SceneImageSourceText.Text;
            SceneItem newScene = new SceneItem(sceneName, ImageSource, currentSceneConfig.ToList());
            availableScenes.Add(newScene);
            currentSceneConfig.Clear();
        }

        private void Button_Click_Add_To_Scene(object sender, RoutedEventArgs e)
        {
            if (ColorPickerGridView.SelectedItem != null && 
                LightPickerGridView.SelectedItem != null)
            {
                List<LightItem> lightsInThisPartOfScene = new List<LightItem>();  
                // TODO: Optimize this atrocity with a better comparison
                foreach (LightItem li in LightPickerGridView.SelectedItems)
                {
                    foreach (var sceneItem in currentSceneConfig)
                    {
                        if (sceneItem.LightsInScene.Contains(li.LightName))
                        {
                            return;
                        }
                    }
                    lightsInThisPartOfScene.Add(li);
                }

                ColorDTO selectedColor = ((ColorItem)(ColorPickerGridView.SelectedItem)).color;
                SceneConfigItem sci = new SceneConfigItem(lightsInThisPartOfScene, selectedColor);
                currentSceneConfig.Add(sci);
            }
        }
    }
}

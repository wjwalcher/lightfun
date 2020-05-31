using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFun.Models
{
    class AppInfoSingleton
    {

        private static ObservableCollection<ColorItem> colorItems;
        private static ObservableCollection<LightItem> lightItems;
        private static ObservableCollection<SceneItem> sceneItems;
        private static ObservableCollection<SceneConfigItem> sceneConfigItems;

        private static AppInfoSingleton theInstance;
        public static AppInfoSingleton GetInstance()
        {
            if (theInstance == null)
            {
                theInstance = new AppInfoSingleton();
            }
            return theInstance;
        }

        private ObservableCollection<LightItem> CreateLightItems()
        {
            HueCommunicationService hcs = new HueCommunicationService();
            List<LightItem> availableLights = hcs.GetAvailableLights();
            ObservableCollection<LightItem> observableLights = new ObservableCollection<LightItem>();
            foreach (var light in availableLights)
            {
                observableLights.Add(light);
            }
            return observableLights;
        }

        public int testScene()
        {
            HueCommunicationService hcs = new HueCommunicationService();
            ObservableCollection<LightItem> lightItems = GetInstance().GetLightItems();
            foreach (var lightItem in lightItems)
            {
                ColorDTO nextColor = ColorDTO.GetRandomColor();
                hcs.CycleLightColor(nextColor, lightItem.LightNumber);
            }

            return 0;
        }
        
        private ObservableCollection<SceneItem> CreateSceneItems()
        {
            ObservableCollection<SceneItem> sceneItems = new ObservableCollection<SceneItem>();
            sceneItems.Add(new SceneItem("Rainbow Mode", 
                "http://www.vowedandamazed.co.uk/wp-content/uploads/2018/02/Rainbow-sign-vowed-amazed-detail-WEB.jpg",  
                testScene));
            return sceneItems;
        }

        private AppInfoSingleton()
        {
            colorItems = new ObservableCollection<ColorItem>();
            lightItems = CreateLightItems();
            sceneItems = CreateSceneItems();
            sceneConfigItems = new ObservableCollection<SceneConfigItem>();
        }

        public ObservableCollection<ColorItem> GetColorItems()
        {
            return colorItems;
        }

        public ObservableCollection<LightItem> GetLightItems()
        {

            return lightItems;
        }

        public ObservableCollection<SceneItem> GetSceneItems()
        {
            return sceneItems;
        }

    }
}

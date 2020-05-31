using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace LightFun.Models
{
    public class SceneItem
    {
        public string SceneName { get; set; }
        public ImageSource SceneImage { get; set; }

        private Dictionary<int, ColorDTO> sceneMappings;

        private Func<int> colorCycleFunc = null;

        // TODO: take lambda as one param to be called in DoScene
        // take in the image as well for background of scene tile
        public SceneItem(string SceneName, string ImageUri, Func<int> colorCycleFunc)
        {
            this.SceneName = SceneName;
            this.colorCycleFunc = colorCycleFunc;

            BitmapImage src = new BitmapImage();
            src.UriSource = new Uri(ImageUri, UriKind.Absolute);

            SceneImage = src;

            this.sceneMappings = new Dictionary<int, ColorDTO>();

        }

        public SceneItem(string SceneName, string ImageUri, List<SceneConfigItem> configItems)
        {
            this.SceneName = SceneName;

            BitmapImage src = new BitmapImage();
            src.UriSource = new Uri(ImageUri, UriKind.Absolute);

            SceneImage = src;

            this.sceneMappings = new Dictionary<int, ColorDTO>();
            foreach (SceneConfigItem sci in configItems)
            {
                foreach (LightItem li in sci.lightsUsed)
                {
                    sceneMappings[li.LightNumber] = sci.colorDTO;
                }
            }

        }

        public void DoScene()
        {
            if (colorCycleFunc != null)
            {
                colorCycleFunc();
            }
            else
            {
                HueCommunicationService hcs = new HueCommunicationService();
                foreach (var key in sceneMappings.Keys)
                {
                    hcs.ChangeLightColor(sceneMappings[key], key);
                }
            }
            
        }

    }
}

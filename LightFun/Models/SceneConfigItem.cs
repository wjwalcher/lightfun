using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace LightFun.Models
{
   
    public class SceneConfigItem
    {
        public List<LightItem> lightsUsed;
        public Color colorUsed;
        public ColorDTO colorDTO;

        public string LightsInScene { 
            get
            {
                string lights = string.Empty;
                for (int i = 0; i < lightsUsed.Count - 1; i++)
                {
                    lights += lightsUsed[i].LightName;
                    lights += ", ";
                }
                lights += lightsUsed[lightsUsed.Count - 1].LightName;
                return lights;
            } 
        }

        public SceneConfigItem(List<LightItem> lightsUsed, ColorDTO colorUsed)
        {
            this.lightsUsed = lightsUsed;
            this.colorDTO = colorUsed;
            this.colorUsed = new Color();
            this.colorUsed.A = 254;
            this.colorUsed.R = colorUsed.r;
            this.colorUsed.G = colorUsed.g;
            this.colorUsed.B = colorUsed.b;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace LightFun.Models
{
    class ColorItem
    {

        public ColorDTO color;
        public SolidColorBrush FillColorValue 
        { 
            get 
            {
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Color.FromArgb(255, color.r, color.g, color.b);
                return brush;
            } 
        }


        public ColorItem(ColorDTO color)
        {
            this.color = color;
        }
    }
}

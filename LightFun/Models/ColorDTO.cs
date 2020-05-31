using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFun.Models
{
    
    public class ColorDTO
    {
        public byte r;
        public byte g;
        public byte b;

        public ColorDTO(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public static ColorDTO GetRandomColor()
        {
            Random random = new Random();
            ColorDTO randColor = new ColorDTO((byte)random.Next(0, 254),
                                              (byte)random.Next(0, 254),
                                              (byte)random.Next(0, 254));
            return randColor;
        }
    }
}

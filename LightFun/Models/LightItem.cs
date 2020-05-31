using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFun.Models
{
    
    public class LightItem
    {
        private string _lightName;
        public string LightName
        {
            get
            {
                return _lightName;
            }

            set
            {
                _lightName = value;
            }
        }

        public int LightNumber;

        public LightItem(string LightName, int LightNumber)
        {
            this.LightName = LightName;
            this.LightNumber = LightNumber;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFun.Models
{

    // DTO representing a user
    // TODO: Determine required fields here
    class User
    {
        public string Name { get; set; }

        public User(string Name)
        {
            this.Name = Name;
        }
    }
}

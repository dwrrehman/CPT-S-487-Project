using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    internal class KeyBind
    {

        public string name { get; set; } 
        public string key { get; set; }
        public string gamepad { get; set; } 

        public KeyBind(string name, string key)
        {
            this.name = name;
            this.key = key;
            //this.gamepad = Gamepad;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    internal class KeyBindList
    {

        public List<KeyBind> keyBinds { get; set; } = new List<KeyBind>();

        public string GetKeyByName(string actionName)
        {
            foreach (var kb in keyBinds)
            {
                if (kb.name == actionName)
                    return kb.key;
            }
            return "";
        }
        public string GetpadByName(string actionName)
        {
            foreach (var kb in keyBinds)
            {
                if (kb.name == actionName)
                    return kb.gamepad;
            }
            return "";
        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    class WaveConfig
    {
        public float Duration { get; set; }
        public float SpawnInterval { get; set; }
        public string EnemyType { get; set; }
        public string Texture { get; set; }
        public string BulletTexture { get; set; }

        public int HP { get; set; }
        public string Move { get; set; }  
        public string Fire { get; set; }

        public int MaxCount { get; set; }

        public string Move2 { get; set; }
        public string Fire2 { get; set; }

        public int Trigger { get; set; }
    }
}

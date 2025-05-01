using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    class Wave
    {

        public float Duration;
        public float SpawnInterval;
        public string EnemyType;
        public int HP;
        public Movement MovementStrategy;
        public Fire FireStrategy;
        public int MaxCount;
        public Texture2D Texture;
        public Texture2D BulletTexture;

        public float Timer;        
        public float SpawnTimer;  
        public int Spawned;

        public WaveConfig Config;
    }
}


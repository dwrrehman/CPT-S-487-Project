using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    class WaveInstruction
    {

        public float SpawnTime { get; set; }    
        public string EnemyType { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool IsSpawned { get; set; } = false;

        public WaveInstruction() { }

        public WaveInstruction(float spawnTime, string enemyType, int x, int y)
        {
            SpawnTime = spawnTime;
            EnemyType = enemyType;
            PositionX = x;
            PositionY = y;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    class Wave
    {

        public float Duration { get; set; }              
        public float SpawnInterval { get; set; }
        public string EnemyType { get; set; }

        public float spawnTimer;
        public bool AlreadySpawned { get; set; }

        public Wave() { }

        public Wave(float duration, string enemyType, float spawnInterval)
        {
            Duration = duration;
            EnemyType = enemyType;
            SpawnInterval = spawnInterval;
            spawnTimer = 0;
            AlreadySpawned = false;
        }
    }
}

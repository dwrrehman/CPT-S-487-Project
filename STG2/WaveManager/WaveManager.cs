using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace STG2
{
     class WaveManager
    {
        private List<Wave> _waves;
        private int _currentWaveIndex;    
        private float _waveTimer;
        private Player _player;
        bool finalBossAlive = false;

        public WaveManager(Player player,List<Wave> waves)
        {
            _waves = waves;
            _currentWaveIndex = 0;
            _waveTimer = 0f;
            _player = player;
        }

        public void Update(GameTime gameTime, List<Enemy> enemies, EntityFactory factory, Texture2D enemy1, Texture2D enemy2, Texture2D midboss, Texture2D finalboss, Texture2D pixel)
        {
            if (_currentWaveIndex >= _waves.Count) return;

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _waveTimer += delta;

            var wave = _waves[_currentWaveIndex];
            wave.spawnTimer += delta;

            if ((wave.EnemyType == "MidBoss" || wave.EnemyType == "FinalBoss"))
            {
                if (!wave.AlreadySpawned)
                {
                    if (wave.spawnTimer >= wave.SpawnInterval)
                    {
                        SpawnEnemy(wave.EnemyType, enemies, factory,
                                   enemy1, enemy2,
                                   midboss, finalboss, pixel);

                        wave.AlreadySpawned = true; 
                    }
                }
            }
            else
            {
                while (wave.spawnTimer >= wave.SpawnInterval)
                {
                    SpawnEnemy(wave.EnemyType, enemies, factory,
                               enemy1, enemy2,
                               midboss, finalboss, pixel);

                    wave.spawnTimer -= wave.SpawnInterval;
                }
            }

            if (_waveTimer >= wave.Duration)
            {
                
                _currentWaveIndex++;
                _waveTimer = 0;
            }

        }
        public bool Finalbossalive(GameTime gameTime, List<Enemy> enemies)
        {
            if (_currentWaveIndex >= _waves.Count)
                return false; 
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _waveTimer += delta;

            var wave = _waves[_currentWaveIndex];
            wave.spawnTimer += delta;

            if (_waveTimer >= wave.Duration)
            {
                if (_currentWaveIndex == _waves.Count - 1)
                {
                    bool finalBossAlive = enemies.Any(e => e is Boss b && !b.IsMidBoss);
                    if (finalBossAlive)
                    {
                        return true;
                    }
                }

                _currentWaveIndex++;
                _waveTimer = 0;
            }
            return false;

        }

        private void SpawnEnemy(string Enemytype,List<Enemy> enemies, EntityFactory factory, Texture2D Renemy1, Texture2D Genemy2, Texture2D midboss, Texture2D finalboss,Texture2D pixel)
        {
            Random rand = new Random();
            float x = rand.Next(50, 400);
            float y = 50;


            switch (Enemytype)
            {
//                case "Special-attack":
//                    var chainEnemy = factory.CreateBoss(
//                        new Vector2(x, y),
//                        finalboss,                // reuse a texture
//                        3,
//                        new DownMovement(),     // or TrackingMovement
//                        new BrokenThreadFire(pixel, spacing: 0.14f, angleStep: 0.12f, angularVel: 0.012f));
//                    enemies.Add(chainEnemy);
//                    break;

                case "Enemy1":
                    var enemy1 = factory.CreateEnemy(new Vector2(x, y), Renemy1, 3, new DownMovement(), new EnemyFire(pixel));
                    enemies.Add(enemy1);

                    break;
                case "Enemy2":
                    var enemy2 = factory.CreateEnemy(new Vector2(x, y), Genemy2, 3, new TrackingMovement(_player), new EnemyFire(pixel));
                    enemies.Add(enemy2);
                    break;
                case "MidBoss":
                    var mb = factory.CreatBoss(new Vector2(x, y), midboss, 150, new HorizonalMovement(), new EnemyFire(pixel));
                    enemies.Add(mb);
                    break;
                case "FinalBoss":
                    var necklace   = new BrokenThreadFire(pixel, spacing:0.24f, angleStep:0.15f);
                    var ringSpray  = new ConcentricRingFire(pixel, bulletsPerRing:24,
                                                            ringInterval:4.0f, bulletSpeed:2.5f);
                    var fb = factory.CreatBoss(new Vector2(x, 250), finalboss, 300, new HorizonalMovement(), new MultiFire(necklace, ringSpray));
                    enemies.Add(fb);
                    break;
            }
        }

    
        public float GetCurrentWaveRemainingTime()
        {
            if (_currentWaveIndex >= _waves.Count)
                return 0f; 

            float dur = _waves[_currentWaveIndex].Duration;
            float remain = dur - _waveTimer;
            if (remain < 0) remain = 0;
            return remain;
        }

        public int GetCurrentWaveIndex() => _currentWaveIndex;

        public static List<Wave> LoadWavesFromJson(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<Wave>();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Wave>>(json);
        }
      
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using STG2.Strategy;
namespace STG2
{
     class WaveManager
    {
        private List<Wave> _waves;
        private int _currentWaveIndex;    
        private Player _player;
        bool finalBossAlive = false;
        private Dictionary<string,Texture2D> _textures;

        public WaveManager(Player player,List<Wave> waves)
        {
            _waves = waves;
            _player = player;
        }

        public void Update(GameTime gameTime, List<Enemy> enemies, EntityFactory factory)
        {
            if (_currentWaveIndex >= _waves.Count) return;

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Wave wave = _waves[_currentWaveIndex];

            wave.Timer += delta;
            wave.SpawnTimer += delta;
            if (wave.EnemyType is "MidBoss" or "FinalBoss" && wave.Spawned < wave.MaxCount)
            {
                SpawnEnemy(wave, enemies, factory);
                wave.Spawned++;
                wave.SpawnTimer -= wave.SpawnInterval;
            }

            else
            {
                if (wave.SpawnTimer >= wave.SpawnInterval && wave.Spawned < wave.MaxCount)
                {
                    SpawnEnemy(wave, enemies, factory);
                    wave.Spawned++;
                    wave.SpawnTimer -= wave.SpawnInterval;

                }
            }

            bool durationDone = wave.Timer >= wave.Duration;
            bool countDone = wave.Spawned >= wave.MaxCount;

            if (wave.EnemyType is "MidBoss" or "FinalBoss")
            {
                wave.Spawned = wave.MaxCount;
                bool bossAlive = enemies.Any(e => e is Boss b && (wave.EnemyType == "MidBoss" ? b._isMidBoss : !b._isMidBoss));

                if (durationDone || !bossAlive)
                    _currentWaveIndex++;
            }
            else
            {
                if (durationDone || countDone)
                    _currentWaveIndex++;
            }
            foreach (var e in enemies)
            {
                if (e is Boss b && !b.Phase2Done && b.TriggerHp > 0 && b.Health <= b.TriggerHp)
                {
                    if (b.NextMove != null) b.MovementStrategy = b.NextMove;
                    if (b.NextFire != null) b.FireStrategy = b.NextFire;

                    b.Phase2Done = true;
                }


            }
        }
        public bool Finalbossalive(GameTime gameTime, List<Enemy> enemies)
        {
            if (_currentWaveIndex >= _waves.Count)
                return false; 
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var wave = _waves[_currentWaveIndex];


            if (wave.Timer >= wave.Duration)
            {
                if (_currentWaveIndex == _waves.Count - 1)
                {
                    bool finalBossAlive = enemies.Any(e => e is Boss b && !b._isMidBoss);
                    if (finalBossAlive)
                    {
                        return true;
                    }
                }

                wave.Timer = 0;
            }
            return false;

        }

        private void SpawnEnemy(Wave wave, List<Enemy> list, EntityFactory factory)
        {
            Random rand = new Random();
            float x = rand.Next(50, 400);
            float y = 50;
            var move = wave.MovementStrategy;
            var gun = wave.FireStrategy;
            Texture2D tex = wave.Texture;
            bool isMid = wave.EnemyType == "MidBoss";

            if (wave.EnemyType is "MidBoss" or "FinalBoss")
            {
                var boss = factory.CreatBoss(
                               new Vector2(x, y),
                               wave.Texture,
                               isMid,
                               wave.HP,
                               wave.MovementStrategy,
                               wave.FireStrategy);

                if (!string.IsNullOrEmpty(wave.Config.Move2) ||
                    !string.IsNullOrEmpty(wave.Config.Fire2))
                {
                    boss.NextMove = MakeMove(wave.Config.Move2 ?? wave.Config.Move, _player);
                    boss.NextFire = MakeFire(wave.Config.Fire2 ?? wave.Config.Fire,
                                               wave.BulletTexture);
                    boss.TriggerHp = wave.Config.Trigger > 0
                                      ? wave.Config.Trigger
                                      : wave.HP / 2; 
                }

                list.Add(boss);
            }
            else
            {
                list.Add(factory.CreateEnemy(new Vector2(x, y),
                                         wave.Texture,
                                         wave.HP,
                                         wave.MovementStrategy,
                                         wave.FireStrategy));
            }

        }

    
        public float GetCurrentWaveRemainingTime()
        {
            if (_currentWaveIndex >= _waves.Count)
                return 0f; 

            float dur = _waves[_currentWaveIndex].Duration;
            float remain = dur - _waves[_currentWaveIndex].Timer;
            if (remain < 0) remain = 0;
            return remain;
        }

        public int GetCurrentWaveIndex() => _currentWaveIndex;

        public static List<Wave> LoadWavesFromJson(string filePath,ContentManager _content, Player player)
        {
            if (!File.Exists(filePath))
                return new List<Wave>();

            var configuration = JsonSerializer.Deserialize<List<WaveConfig>>
             (File.ReadAllText(filePath));
            return configuration.Select(c => new Wave
            {
                Config = c,
                Duration = c.Duration,
                SpawnInterval = c.SpawnInterval,
                Texture = _content.Load<Texture2D>(c.Texture),
                BulletTexture = _content.Load<Texture2D>(c.BulletTexture),
                EnemyType = c.EnemyType,
                HP = c.HP,
                MovementStrategy = MakeMove(c.Move, player),   
                FireStrategy = MakeFire(c.Fire, _content.Load<Texture2D>(c.BulletTexture)),
                MaxCount = c.MaxCount,
            }).ToList();
        }
         private static Movement MakeMove(string key, Player p) => key switch{
            "DownMovement" => new DownMovement(),
            "TrackingMovement" => new TrackingMovement(p),
            "HorizonalMovement" => new HorizonalMovement(),
            _ => throw new ArgumentException()
         };

         private static Fire MakeFire(string key, Texture2D bullet) => key switch{
            "Single" => new EnemyFire(bullet),
            "BrokenThreadFire" => new BrokenThreadFire(bullet),
            "ConcentricRingFire" => new ConcentricRingFire(bullet),
            _ => throw new ArgumentException()
         };
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using STG;
using STG2.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{
    
    internal class GamePlay:MenuScreen{
        private PlayerPlanne player;
        private Texture2D playerImage;
        private List<Enemy> _enemies;
        private List<PlayerBullet> _bullets;
        private Texture2D _enemyTexture;
        private Texture2D _enemyTextureGreen;
        public static Texture2D PixelTexture;
        private Texture2D _midBossTexture;
        private Texture2D _FinalBossTexture;
        private double _enemySpawnTimer;
        private double _midBossSpawnTimer;
        private bool _midBossSpawned = true;
        private double _FinalBossSpawnTimer;
        private bool _FinalBossSpawned = true;
        private double start = 0;
        private double Cooldown = 0.3;
        private KeyboardState _currentKeyboard;
        private KeyboardState _previousKeyboard;
        public GamePlay(Game1 game1) : base(game1)
        {
        }

        public override void Show()
        {
            base.Show();
            playerImage = Game1.Content.Load<Texture2D>("plane");
            _speed = 5;
            _background = Game1.Content.Load<Texture2D>("background3");
            player = new PlayerPlanne(position: new Vector2(200, 750), texture: playerImage,
                health: 10,
                speed: 5,
                STG.direction.Up
            );

            _enemies = new List<Enemy>();
            _bullets = new List<PlayerBullet>();

            _enemyTexture = new Texture2D(Game1.GraphicsDevice, 50, 50);
            Color[] data = new Color[50 * 50];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Red;
            _enemyTexture.SetData(data);

            _midBossTexture = new Texture2D(Game1.GraphicsDevice, 100, 100);
            Color[] datamid = new Color[100 * 100];
            for (int i = 0; i < datamid.Length; ++i) datamid[i] = Color.Yellow;
            _midBossTexture.SetData(datamid);

            _FinalBossTexture = new Texture2D(Game1.GraphicsDevice, 150, 150);
            Color[] dataFin = new Color[150 * 150];
            for (int i = 0; i < dataFin.Length; ++i) dataFin[i] = Color.Brown;
            _FinalBossTexture.SetData(dataFin);

            _enemyTextureGreen = new Texture2D(Game1.GraphicsDevice, 50, 50);
            Color[] greenData = new Color[50 * 50];
            for (int i = 0; i < greenData.Length; ++i) greenData[i] = Color.Green;
            _enemyTextureGreen.SetData(greenData);

            PixelTexture = new Texture2D(Game1.GraphicsDevice, 1, 1);
            PixelTexture.SetData(new[] { Color.White });
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _previousKeyboard = _currentKeyboard;
            _currentKeyboard = Keyboard.GetState();
            if (_currentKeyboard.IsKeyDown(Keys.Escape))
            {
                Game1.ScreenManager.ChangeScreen(new Menu(Game1));

            }
            player.Update();
            _enemySpawnTimer += gameTime.ElapsedGameTime.TotalSeconds;
            _midBossSpawnTimer += gameTime.ElapsedGameTime.TotalSeconds;
            _FinalBossSpawnTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (_enemySpawnTimer >= 2) // Spawn an enemy every 2 seconds
            {
                int enemyXPosition = new Random().Next(50, 400);

                // Alternate between spawning Red and Green enemies
                if (_enemies.Count % 2 == 0)
                {
                    // Red enemy moves **straight down**
                    _enemies.Add(new Enemy(new Vector2(enemyXPosition, 50), _enemyTexture, 3, 2, direction.Down, 1.5, EnemyType.Downward));
                }
                else
                {
                    // Green enemy moves **side-to-side in mid-screen**
                    _enemies.Add(new Enemy(new Vector2(0, 300), _enemyTextureGreen, 3, 2, direction.Right, 1.5, EnemyType.SideToSide));
                }

                _enemySpawnTimer = 0;
                //_enemies.Add(new Enemy(new Vector2(Random.Shared.Next(50, 400), 50), _texture3, 3, 2, direction.Down, 1.5));
                //_enemySpawnTimer = 0;
            }
            if (_enemySpawnTimer >= 2) // Spawn an enemy every 2 seconds
            {
                int enemyXPosition = new Random().Next(50, 400);

                // Alternate between spawning Red and Green enemies
                if (_enemies.Count % 2 == 0)
                {
                    // Red enemy moves **straight down**
                    _enemies.Add(new Enemy(new Vector2(enemyXPosition, 50), _enemyTexture, 3, 2, direction.Down, 1.5, EnemyType.Downward));
                }
                else
                {
                    // Green enemy moves **side-to-side in mid-screen**
                    _enemies.Add(new Enemy(new Vector2(0, 300), _enemyTextureGreen, 3, 2, direction.Right, 1.5, EnemyType.SideToSide));
                }

                _enemySpawnTimer = 0;
                //_enemies.Add(new Enemy(new Vector2(Random.Shared.Next(50, 400), 50), _texture3, 3, 2, direction.Down, 1.5));
                //_enemySpawnTimer = 0;
            }
            if (_midBossSpawnTimer >= 10 && _midBossSpawned)
            {
                int enemyXPosition = new Random().Next(50, 400);
                _enemies.Add(new Enemy(new Vector2(enemyXPosition, 50), _midBossTexture, 3, 2, direction.Down, 1.5, EnemyType.SideToSide));
                _midBossSpawned = false;

            }
            if (_FinalBossSpawnTimer >= 15 && _FinalBossSpawned)
            {
                int enemyXPosition = new Random().Next(50, 400);
                _enemies.Add(new Enemy(new Vector2(enemyXPosition, 50), _FinalBossTexture, 3, 2, direction.Down, 1.5, EnemyType.SideToSide));
                _FinalBossSpawned = false;

            }
            // Update enemies
            foreach (var enemy in _enemies)
            {
                enemy.Update(gameTime);
            }

        }
        

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            base.Draw(gameTime, spriteBatch);

            
            player.Draw(spriteBatch);
          
            foreach (var enemy in _enemies)
            {
                enemy.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}

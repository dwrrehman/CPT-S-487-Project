using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG2
{

    internal class GamePlay : MenuScreen
    {
        private Player player;
        private Texture2D playerImage;

        private List<Enemy> _enemies;
        private List<Bullet> _bullets;

        private Texture2D _enemyTexture;

        private Texture2D _enemyTextureGreen;
        private Texture2D _bulletTexture;
        public Texture2D PixelTexture;
        private Texture2D _midBossTexture;
        private Texture2D _FinalBossTexture;
        private double _enemySpawnTimer;
        private double _midBossSpawnTimer;
        private bool _midBossSpawned = true;
        private double _FinalBossSpawnTimer;
        private bool _FinalBossSpawned = true;
        private KeyboardState _currentKeyboard;
        private GamePadState _currentGamePad;

        EntityFactory _entityFactory = new RegularFactory();

        private CollisionManager _collisionManager;

        private HealthBar _healthBar;

        public GamePlay(Game1 game1) : base(game1)
        {
        }

        public override void Show()
        {
            base.Show();
            _speed = 5;
            _background = Game1.Content.Load<Texture2D>("background3");
            playerImage = Game1.Content.Load<Texture2D>("plane");
            _bulletTexture = Game1.Content.Load<Texture2D>("missile");
            player = _entityFactory.CreatePlayer(playerImage);
            _enemyTexture = Game1.Content.Load<Texture2D>("Enemy1");
            _enemyTextureGreen = Game1.Content.Load<Texture2D>("Enemy2");
            _enemies = new List<Enemy>();
            _bullets = new List<Bullet>();


            _midBossTexture = Game1.Content.Load<Texture2D>("Enemy1");  // Use Enemy1 for mid boss
            _FinalBossTexture = Game1.Content.Load<Texture2D>("Enemy2"); // Use Enemy2 for final boss

            PixelTexture = Game1.Content.Load<Texture2D>("eb");

            player.FireStrategy = new RegularFire(_bulletTexture);

            // Initialize the collision manager
            _collisionManager = new CollisionManager(player, _enemies, _bullets);

            _healthBar = new HealthBar(50, new Vector2(20, 20), 200, 20);
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _currentKeyboard = Keyboard.GetState();
            _currentGamePad = GamePad.GetState(PlayerIndex.One);
            if (_currentKeyboard.IsKeyDown(Keys.Escape) || _currentGamePad.IsButtonDown(Buttons.Start))
            {
                Game1.ScreenManager.ChangeScreen(new Menu(Game1));
            }

            player.Update(gameTime, _bullets, 0.3);

            _enemySpawnTimer += gameTime.ElapsedGameTime.TotalSeconds;
            _midBossSpawnTimer += gameTime.ElapsedGameTime.TotalSeconds;
            _FinalBossSpawnTimer += gameTime.ElapsedGameTime.TotalSeconds;

            // Regular enemy spawning logic
            if (_enemySpawnTimer >= 2) // Spawn an enemy every 2 seconds
            {
                int enemyXPosition = new Random().Next(50, 400);

                // Alternate between spawning Red and Green enemies
                if (_enemies.Count % 2 == 0)
                {
                    // Red enemy moves straight down
                    Enemy newEnemy = _entityFactory.CreateEnemy(new Vector2(enemyXPosition, 50), _enemyTexture, 3, new DownMovement());
                    newEnemy.FireStrategy = new EnemyFire(PixelTexture);
                    _enemies.Add(newEnemy);
                }
                else
                {
                    // Green enemy moves side-to-side in mid-screen
                    Enemy newEnemy = _entityFactory.CreateEnemy(new Vector2(0, 300), _enemyTextureGreen, 3, new HorizonalMovement());
                    newEnemy.FireStrategy = new EnemyFire(PixelTexture);
                    _enemies.Add(newEnemy);
                }

                _enemySpawnTimer = 0;
            }

            // Mid boss spawning logic - spawn after 30 seconds
            if (_midBossSpawnTimer >= 30 && _midBossSpawned)
            {
                int bossXPosition = new Random().Next(100, 300);
                _midBossSpawned = false;

                // Create mid boss with horizontal movement
                Boss midBoss = _entityFactory.CreatBoss(new Vector2(bossXPosition, 100), _midBossTexture, 150);
                midBoss.MovementStrategy = new HorizonalMovement();
                midBoss.FireStrategy = new EnemyFire(PixelTexture);

                // Add to enemies list since it's treated as an enemy in the collision system
                _enemies.Add(midBoss);

               // Console.WriteLine("Mid Boss spawned!");
            }

            // Final boss spawning logic - spawn after 60 seconds
            if (_FinalBossSpawnTimer >= 60 && _FinalBossSpawned)
            {
                int bossXPosition = new Random().Next(100, 300);
                _FinalBossSpawned = false;

                // Create final boss with horizontal movement
                Boss finalBoss = _entityFactory.CreatBoss(new Vector2(bossXPosition, 100), _FinalBossTexture, 300);
                finalBoss.MovementStrategy = new HorizonalMovement();
                finalBoss.FireStrategy = new EnemyFire(PixelTexture);
               
                // Add to enemies list
                _enemies.Add(finalBoss);

               // Console.WriteLine("Final Boss spawned!");
            }

            // Update enemies
            foreach (var enemy in _enemies)
            {
                enemy.Update(gameTime, _bullets, 1);
            }

            // Update bullets
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                _bullets[i].Update();

                if (_bullets[i].Position.X < 0 ||_bullets[i].Position.X > 450  ||_bullets[i].Position.Y <0 ||_bullets[i].Position.Y > 850)
                {
                    _bullets.RemoveAt(i);
                    continue;
                }
            }

            // Check for collisions
            _collisionManager.CheckCollisions();

            // Remove dead enemies
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                var enemy = _enemies[i];
                if (_enemies[i].Health <= 0)
                {
                    if (enemy is Boss boss)
                    {
                        if (!boss.IsMidBoss)
                        {
                            Game1.ScreenManager.ChangeScreen(new WinScreen(Game1));
                            return; 
                        }
                    }


                    _enemies.RemoveAt(i);
                }
            }

              
                
            

            // Remove dead bullets
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                if (_bullets[i].Health <= 0)
                {
                    _bullets.RemoveAt(i);
                }
            }

            // Check if player is dead
            if (player.Health <= 0)
            {
                // Game over - go back to menu
                Game1.ScreenManager.ChangeScreen(new LoseScreen(Game1));
            }
          
            _healthBar.Update(player.Health);
           // Console.WriteLine($"Active bullets: {_bullets.Count} (Player: {_bullets.Count(b => b.MovementStrategy is UpMovement)}, Enemy: {_bullets.Count(b => !(b.MovementStrategy is UpMovement))})");
           // Console.WriteLine($"Player health: {player.Health}");
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            base.Draw(gameTime, spriteBatch);
            _healthBar.Draw(spriteBatch);
            player.Draw(spriteBatch);

            foreach (var enemy in _enemies)
            {
                enemy.Draw(spriteBatch);
            }

            foreach (var bullet in _bullets)
            {
                bullet.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
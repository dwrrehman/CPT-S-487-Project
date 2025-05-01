using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STG2.Character;
namespace STG2
{

    internal class GamePlay : Screen
    {
        private Player player;
        private Texture2D playerImage;

        private List<Enemy> _enemies;
        private List<Bullet> _bullets;

        private List<Buff> _buff;

        private BombManager _bombManager;
        private Texture2D _enemyTexture;
        private Texture2D _bombIcon;

        private Texture2D _enemyTextureGreen;
        private Texture2D _bulletTexture;
        public Texture2D PixelTexture;
        public Texture2D Texture;

        private Texture2D _midBossTexture;
        private Texture2D _FinalBossTexture;
        private double _enemySpawnTimer;
        private double _midBossSpawnTimer;
        private bool _midBossSpawned = true;
        private double _FinalBossSpawnTimer;
        private bool _FinalBossSpawned = true;
        private KeyboardState _currentKeyboard;
        private GamePadState _currentGamePad;
        private WaveManager _waveManager;
        EntityFactory _entityFactory = new RegularFactory();
        private SpriteFont _font;

        private CollisionManager _collisionManager;
        private DropManagercs _dropManager;

        private HealthBar _healthBar;

        public GamePlay(Game1 game1) : base(game1)
        {
        }

    

        public override void Show()
        {
            base.Show();
            _speed = 5;
            _background = Game1.Content.Load<Texture2D>("jojo3");
            playerImage = Game1.Content.Load<Texture2D>("jojo2");
            _bulletTexture = Game1.Content.Load<Texture2D>("missile");
            Texture = Game1.Content.Load<Texture2D>("missile");
            player = _entityFactory.CreatePlayer(playerImage);
            _enemies = new List<Enemy>();
            _bullets = new List<Bullet>();
            _font = Game1.Content.Load<SpriteFont>("Fonts");

            _buff = new List<Buff>();

            PixelTexture = Game1.Content.Load<Texture2D>("eb");

            player.FireStrategy = new RegularFire(_bulletTexture);
            _bombManager = new BombManager(Game1.GraphicsDevice, _font);
            _bombIcon = Game1.Content.Load<Texture2D>("missile");




            var waves = WaveManager.LoadWavesFromJson(
               "Gameplay.json",
               Game1.Content, player);
             

            _waveManager = new WaveManager(player, waves);
            _dropManager = new DropManagercs(_buff, Game1.Content);

            foreach (var e in _enemies)
                e.RegisterObserver(_dropManager);

            // Initialize the collision manager
            _collisionManager = new CollisionManager(player, _enemies, _bullets, _buff);

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

            player.Update(gameTime, _bullets,0.3,_enemies,_bombManager);
            _waveManager.Update(gameTime, _enemies, _entityFactory);


            foreach (var enemy in _enemies)
            {
                enemy.Update(gameTime, _bullets, 1);
            }
            // Update bullets
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                _bullets[i].Update();
                int w = Game1.GraphicsDevice.Viewport.Width;
                int h = Game1.GraphicsDevice.Viewport.Height;
                if (_bullets[i].Position.X < 0 || _bullets[i].Position.X > w || _bullets[i].Position.Y < 0 || _bullets[i].Position.Y > h)
                {
                    _bullets.RemoveAt(i);
                    continue;
                }
            }

            // Check for collisions
            _collisionManager.CheckCollisions();
            for (int i = _buff.Count - 1; i >= 0; i--)
            {
                var b = _buff[i];
                b.Update(gameTime);

                bool offscreen = b.Position.Y > Game1.GraphicsDevice.Viewport.Height;
                if (b.IsPicked || offscreen)
                    _buff.RemoveAt(i);
            }
            // Remove dead enemies
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                var enemy = _enemies[i];
                if (_enemies[i].Health <= 0)
                {
                    
                    if (enemy is Boss boss)
                    {
                        if (!boss._isMidBoss)
                        {
                            Game1.ScreenManager.ChangeScreen(new WinScreen(Game1));
                            return;
                        }
                    }

                    _dropManager.OnEnemyDied(enemy);
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
            bool isTimeOutLose = _waveManager.Finalbossalive(gameTime, _enemies);

            if (player.Health <= 0 || isTimeOutLose)
            {
                // Game over - go back to menu
                Game1.ScreenManager.ChangeScreen(new LoseScreen(Game1));
            }

            _healthBar.Update(player.Health);
            // Console.WriteLine($"Active bullets: {_bullets.Count} (Player: {_bullets.Count(b => b.MovementStrategy is UpMovement)}, Enemy: {_bullets.Count(b => !(b.MovementStrategy is UpMovement))})");
            // Console.WriteLine($"Player health: {player.Health}");

            player.Update(gameTime, _bullets, 0.3, _enemies, _bombManager);
            _bombManager.Update(gameTime);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            base.Draw(gameTime, spriteBatch);
            _healthBar.Draw(spriteBatch);

            Vector2 bombPos = new Vector2(20, 50);           // below the health bar
            spriteBatch.DrawString(_font, $"x {player.Bombs}", bombPos + new Vector2(34, 4), Color.White);

            if (_bombIcon != null)
                spriteBatch.Draw(_bombIcon,
                    new Rectangle((int)bombPos.X, (int)bombPos.Y, 28, 28),
                    Color.White);

            float remain = _waveManager.GetCurrentWaveRemainingTime();
            int waveIndex = _waveManager.GetCurrentWaveIndex()+1;

            string waveText = $"Wave {waveIndex}: {remain:F1} s left";
            Vector2 textPos = new Vector2(230, 25);

            spriteBatch.DrawString(_font, waveText, textPos, Color.White);

            player.Draw(spriteBatch);

            foreach (var enemy in _enemies)
            {
                enemy.Draw(spriteBatch);
            }

            foreach (var bullet in _bullets)
            {
                bullet.Draw(spriteBatch);
            }
            _bombManager.Draw(spriteBatch);

            foreach (var item in _buff)
                item.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}




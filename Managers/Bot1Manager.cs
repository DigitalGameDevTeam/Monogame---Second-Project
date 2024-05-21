namespace Awesome_Game
{
    public static class Bot1Manager
    {
        public static List<Bot1> Bots1 { get; } = new();
        private static List<Texture2D> textures;
        //private static Texture2D _texture;
        private static float _spawnCooldown;
        private static float _spawnTime;
        private static Random _random;
        private static int _padding;

        public static void Init()
        {
            /*_texture = Globals.Content.Load<Texture2D>("bot1");*/

            textures = new List<Texture2D>
            {
                Globals.Content.Load<Texture2D>("bot2"),
                Globals.Content.Load<Texture2D>("bot3")

            };


            _spawnCooldown = LevelManager.Instance.bot1_SpawnRate;
            _spawnTime = _spawnCooldown;
            _random = new();
            _padding = textures[0].Width / 2;
        }

        private static Vector2 RandomPosition()
        {
            float w = Globals.Bounds.X;
            float h = Globals.Bounds.Y;
            Vector2 pos = new();

            if (_random.NextDouble() < w / (w + h))
            {
                pos.X = (int)(_random.NextDouble() * w);
                pos.Y = (int)(_random.NextDouble() < 0.5 ? -_padding : h + _padding);
            }
            else
            {
                pos.Y = (int)(_random.NextDouble() * h);
                pos.X = (int)(_random.NextDouble() < 0.5 ? -_padding : w + _padding);
            }

            return pos;
        }

        public static void AddBot()
        {
            Bots1.Add(new Bot1(textures, RandomPosition()));
        }

        public static void Update(Player player, GameTime gameTime)
        {
            _spawnTime -= Globals.TotalSeconds;
            while (_spawnTime <= 0)
            {
                _spawnTime += _spawnCooldown;
                AddBot();
            }

            foreach (var bot in Bots1)
            {
                bot.Update(player, gameTime);
            }
            Bots1.RemoveAll(bot => bot.HP <= 0);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var bot in Bots1)
            {
                bot.Draw(spriteBatch);
            }
        }
    }
}

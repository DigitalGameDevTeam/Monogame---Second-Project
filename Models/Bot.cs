using System.Diagnostics;

namespace Awesome_Game
{
    public class Bot : AnimatedSprite
    {
        public int HP { get; private set; }
        public float maxHP { get; private set; }
        public int Speed { get; set; }
        private List<Texture2D> textures;
        public Texture2D healthBarTexture;
        private int currentTextureIndex;

        public Bot(List<Texture2D> tex, Vector2 pos) : base(pos)
        {
            textures = tex;
            currentTextureIndex = 0;
            sTexture = textures[currentTextureIndex];
            Speed = LevelManager.Instance.bot_MovementSpeed;
            HP = LevelManager.Instance.bot_HP;
            maxHP = HP;
            FramesPerSecond = 10;
            AddAnimation(8);

            ChangeTexture(new Random().Next(textures.Count));
        }

        public void LoadContent(ContentManager content)
        {
            textures.Add(Globals.Content.Load<Texture2D>("bot2"));
            textures.Add(Globals.Content.Load<Texture2D>("bot3"));

            healthBarTexture = Globals.Content.Load<Texture2D>("brown");
        }
  
        public void ChangeTexture(int index)
        {

            if (index >= 0 && index < textures.Count)
            {
                currentTextureIndex = index;
                sTexture = textures[currentTextureIndex];
                AddAnimation(8);
            }
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
        }

        public void Update(Player player, GameTime gameTime)
        {
            base.Update(gameTime);

            var toPlayer = player.Position - Position;
            Rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

            if (toPlayer.Length() > 4)
            {
                var dir = Vector2.Normalize(toPlayer);
                Position += dir * Speed * Globals.TotalSeconds;
            }

            if (toPlayer.Length() < 32)
            {
                player.TakeDamage(10);
                HP = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                sTexture,
                Position,
                sRectangles[frameIndex],
                Color.White,
                Rotation + MathHelper.PiOver2,
                new Vector2(sRectangles[frameIndex].Width / 2, sRectangles[frameIndex].Height / 2),
                1f,
                SpriteEffects.None,
                0f
            );

            DrawHealthBar(spriteBatch, Position, 50, 5);
        }

        public void DrawHealthBar(SpriteBatch spriteBatch, Vector2 position, int width, int height)
        {
            Rectangle backgroundBar = new Rectangle((int)position.X - width / 2, (int)position.Y - height - 30, width, height);

            float healthPercentage = (float)HP / LevelManager.Instance.bot_HP;
            int healthBarWidth = (int)(width * healthPercentage);
            Rectangle healthBar = new Rectangle((int)position.X - width / 2, (int)position.Y - height - 30, healthBarWidth, height);

            spriteBatch.Draw(healthBarTexture, backgroundBar, Color.Gray);

            spriteBatch.Draw(healthBarTexture, healthBar, Color.Red);
        }
    }
}

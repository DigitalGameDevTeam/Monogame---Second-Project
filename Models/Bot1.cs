using System.Diagnostics;

namespace Awesome_Game
{
    public class Bot1 : AnimatedSprite
    {
        public int HP { get; private set; }
        public int Speed { get; set; }
        private List<Texture2D> textures;
        private int currentTextureIndex;

        public Bot1(List<Texture2D> tex, Vector2 pos) : base(pos)
        {
            textures = tex;
            currentTextureIndex = 0;
            sTexture = textures[currentTextureIndex];
            Speed = LevelManager.Instance.bot1_MovementSpeed;
            HP = LevelManager.Instance.bot1_HP;
            FramesPerSecond = 10;
            AddAnimation(8);

            ChangeTexture(new Random().Next(textures.Count));
        }

        public void LoadContent(ContentManager content)
        {
            /*sTexture = Globals.Content.Load<Texture2D>("bot1");
            sTexture = Globals.Content.Load<Texture2D>("bot2");*/
            textures.Add(Globals.Content.Load<Texture2D>("bot2"));
            textures.Add(Globals.Content.Load<Texture2D>("bot3"));
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
        }
    }
}

namespace Awesome_Game
{
    public abstract class AnimatedSprite
    {
        protected Texture2D sTexture;
        protected Vector2 sPosition;
        protected Rectangle[] sRectangles;
        protected int frameIndex;
        private double timeElapsed;
        private double timeToUpdate;
        
        public float Rotation { get; set; }
        public Vector2 Position
        {
            get => sPosition;
            set => sPosition = value;
        }

        public int FramesPerSecond
        {
            set { timeToUpdate = 1f / value; }
        }

        public AnimatedSprite(Vector2 position)
        {
            sPosition = position;
        }

        public void AddAnimation(int frames)
        {
            int width = sTexture.Width / frames;
            sRectangles = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                sRectangles[i] = new Rectangle(i * width, 0, width, sTexture.Height);
            }
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (frameIndex < sRectangles.Length - 1)
                    frameIndex++;
                else    
                    frameIndex = 0;
            }
        }
    }
}

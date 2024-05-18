namespace Awesome_Game;

public abstract class AnimatedSprite
{
    protected Texture2D sTexture;
    protected Vector2 sPosition;
    protected Vector2 playerPosition;
    protected Rectangle[] sRectangles;
    protected int frameIndex;

    private double timeElapsed;
    private double timeToUpdade;
    public int FramesPerSecond
    {
        set { timeToUpdade = (1f / value); }
    }
    //protected Vector2 sDirection = Vector2.Zero;

    public AnimatedSprite(Vector2 position)
    {
        sPosition = position;
    }

    public void AddAnimation(int frames)
    {
        int Width = sTexture.Width / frames;
        sRectangles = new Rectangle[frames];
        for (int i = 0; i < frames; i++)
        {
            sRectangles[i] = new Rectangle(i * Width, 0, Width, sTexture.Height);
        }
    }

    public void Update(GameTime gameTime)
    {
        timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

        if (timeElapsed > timeToUpdade)
        {
            timeElapsed -= timeToUpdade;

            if (frameIndex < sRectangles.Length - 1)
                frameIndex++;
            else    
                frameIndex = 0;
        }
    }
}
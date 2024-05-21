namespace Awesome_Game;

public class Sprite(Texture2D tex, Vector2 pos) : AnimatedSprite(pos)
{
    protected readonly Texture2D _texture = tex;
    protected readonly Vector2 origin = new(tex.Width / 2, tex.Height / 2);
    public float Scale { get; set; } = 0.5f;
    public Color Color { get; set; } = Color.White;
    public void Draw()
    {
        Globals.SpriteBatch.Draw(_texture, Position, null, Color, Rotation, origin, Scale, SpriteEffects.None, 1);
    }
}
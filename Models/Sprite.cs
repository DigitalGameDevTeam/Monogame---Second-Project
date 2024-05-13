using System.Data.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Awesome_Game;

public class Sprite
{
    protected readonly Texture2D _texture;
    protected readonly Vector2 origin;
    public Vector2 Position { get; set; }
    public int Speed { get; set; }
    public float Rotation { get; set; }

    public Sprite(Texture2D tex, Vector2 pos)
    {
        _texture = tex;
        Position = pos;
        Speed = 300;
        origin = new(tex.Width / 2, tex.Height / 2);
    }

    public virtual void Draw()
    {
        Globals.SpriteBatch.Draw(_texture, Position, null, Color.White, Rotation, origin, 1, SpriteEffects.None, 1);
    }
}
namespace Awesome_Game;

public class Bot1 : MovingSprite
{
 public Bot1(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        Speed = 100;
    }

    public void Update(Player player)
    {
        var toPlayer = player.Position - Position;
        Rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

        if (toPlayer.Length() > 4)
        {
            var dir = Vector2.Normalize(toPlayer);
            Position += dir * Speed * Globals.TotalSeconds;
        }
    }
}
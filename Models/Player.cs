namespace Awesome_Game;

public class Player : MovingSprite
{
    private Vector2 _minPos, _maxPos;
    public Player(Texture2D tex) : base(tex, GetStartPosition())
    {
        
    }

    private static Vector2 GetStartPosition()
    {
        return new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2);
    }
    private void Fire()
    {
        ProjectileData pd = new()
        {
            Position = Position,
            Rotation = Rotation,
            Lifespan = 2,
            Speed = 600
        };

        ProjectileManager.AddProjectile(pd);
    }

    public void SetBounds(Point mapSize, Point tileSize)
    {
        _minPos = new((-tileSize.X / 2) + origin.X, (-tileSize.Y / 2) + origin.Y);
        _maxPos = new(mapSize.X - (tileSize.X / 2) - origin.X, mapSize.Y - (tileSize.X / 2) - origin.Y);
    }

    public void Update()
    {
        if (InputManager.Direction != Vector2.Zero)
        {
            var dir = Vector2.Normalize(InputManager.Direction);
            Position = new(
                MathHelper.Clamp(Position.X + (dir.X * Speed * Globals.TotalSeconds), 0, Globals.Bounds.X),
                MathHelper.Clamp(Position.Y + (dir.Y * Speed * Globals.TotalSeconds), 0, Globals.Bounds.Y)
            );
        }

        var toMouse = InputManager.MousePosition - Position;
        Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);
        
         if (InputManager.MouseClicked)
        {
            Fire();
        }
    }
}
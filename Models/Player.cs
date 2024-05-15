namespace Awesome_Game;

public class Player : MovingSprite
{
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

        //registra a posiçao do ponteiro e a guarda no formato Point 
        MouseState mouseState = Mouse.GetState();
        Point mousePosition = new(mouseState.X, mouseState.Y);
        //cria um ponto no canto superior esquerdo da tela
        Point topLeft = new Point(0, 0);

        //verifica se o ponteiro está dentro da tela
        if (mousePosition.X >= topLeft.X && mousePosition.X <= Globals.Bounds.X &&
                   mousePosition.Y >= topLeft.Y && mousePosition.Y <= Globals.Bounds.Y)
        {
            //atualiza a posição do ponteiro
            var toMouse = InputManager.MousePosition - Position;
            Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);

            if (InputManager.MouseClicked)
            {
                //atira um projetil
                Fire();
            }
        }
    }
}
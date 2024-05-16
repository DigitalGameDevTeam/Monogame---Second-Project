namespace Awesome_Game;

public class Player : MovingSprite
{
    private Vector2 _minPos, _maxPos;
    private readonly float cooldown;
    private float cooldownLeft;
    public readonly int maxAmmo;
    public int Ammo { get; private set; }
    public readonly float reloadTime;
    public bool isReloading { get; private set; }
    public float reloadTimeLeft;

    public Player(Texture2D tex) : base(tex, GetStartPosition())
    {
        cooldown = 0.25f;
        cooldownLeft = 0f;
        maxAmmo = 20;
        Ammo = maxAmmo;
        reloadTime = 2f;
        isReloading = false;
        reloadTimeLeft = 0f;
    }

    private void Reload()
    {
        if (isReloading) return;
        cooldownLeft = reloadTime;
        isReloading = true;
        Ammo = 0; //iguala a 0 para que durante o tempo de Reload o Visual Ui mostre 0
    }
    private static Vector2 GetStartPosition()
    {
        return new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2);
    }

    private void Fire()
    {
        if (cooldownLeft > 0 || isReloading) return;
        Ammo--;
        if (Ammo > 0)
        {
            cooldownLeft = cooldown;
        }
        else
        {
            Reload();
        }

        ProjectileData pd = new()
        {
            Position = Position,
            Rotation = Rotation,
            Lifespan = 2,
            Speed = 1000
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
        if (cooldownLeft > 0)
        {
            cooldownLeft -= Globals.TotalSeconds;
        }
        else if (isReloading)
        {
            reloadTimeLeft += Globals.TotalSeconds;
            if (reloadTimeLeft >= 0) //verifica se o tempo de reload acabou
            {
                Ammo = maxAmmo;
                isReloading = false;
            }
            
        }

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

            if (InputManager.MouseLeftDown)
            {
                //atira um projetil
                Fire();
            }
            if (InputManager.MouseRightClicked)
            {
                //recarrega a arma
                Reload();
            }
        }
    }
}
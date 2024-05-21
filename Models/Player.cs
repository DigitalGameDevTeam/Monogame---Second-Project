namespace Awesome_Game;

public class Player : Sprite
{

    private Vector2 _minPos, _maxPos;
    public int playerSpeed { get; set; }
    public int Hp { get; private set; }
    private float cooldown;
    private float cooldownLeft;
    public int maxAmmo;
    public int Ammo { get; private set; }
    public float reloadTime;
    public bool isReloading { get; private set; }
    public float reloadTimeLeft;
    public int levelUpGap = 10;

    public Rectangle playerRectangle;

    public Player(Texture2D texture) : base(texture, GetStartPosition())
    {
        playerSpeed = PlayerStats.Instance.player_Speed;
        //Hp = PlayerStats.Instance.player_HP;
        cooldown = PlayerStats.Instance.player_cooldown;
        cooldownLeft = 0f;
        maxAmmo = PlayerStats.Instance.player_maxAmmo;
        Ammo = maxAmmo;
        reloadTime = PlayerStats.Instance.player_reloadTime;
        isReloading = false;
        reloadTimeLeft = 0f;
        FramesPerSecond = 10;

    }
    public void upgradeStats()
    {
        if (GameStats.Instance.Kills == levelUpGap)
        {
            PowerUpsManager.Instance.levelUp();
            levelUpGap += 10;
        }
        playerSpeed = PlayerStats.Instance.player_Speed;
        //Hp = PlayerStats.Instance.player_HP;
        cooldown = PlayerStats.Instance.player_cooldown;
        maxAmmo = PlayerStats.Instance.player_maxAmmo;
        reloadTime = PlayerStats.Instance.player_reloadTime;
    }
    

    public void LoadContent(ContentManager content)
    {
        sTexture = Globals.Content.Load<Texture2D>("player");
        AddAnimation(6);
    }

    public void TakeDamage(int damage)
    {
        PlayerStats.Instance.player_HP -= damage;
        if (PlayerStats.Instance.player_HP <= 0)
        {
            PlayerStats.Instance.player_HP = 0;
            Game1.Instance.GameOver();
        }
    }

    private void Reload()
    {
        SoundManager.Instance.PlayPlayerReload();
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
            SoundManager.Instance.PlayPlayerShoot();
            cooldownLeft = cooldown;
        }
        else
        {
            Reload();
        }

        ProjectileData pd = new()
        {
            Position = sPosition,
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


    public void Update(GameTime gameTime)
    {
        upgradeStats();


        HandleInput(Keyboard.GetState(), gameTime);

        Position = sPosition;

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
                reloadTimeLeft = 0;
            }

        }

        //registra a posiçao do psonteiro e a guarda no formato Point 
        MouseState mouseState = Mouse.GetState();
        Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
        Vector2 toMouse = mousePosition - Position;
        Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);

        //cria um ponto no canto superior esquerdo da tela
        Point topLeft = new Point(0, 0);

        //verifica se o ponteiro está dentro da tela
        if (mousePosition.X >= topLeft.X && mousePosition.X <= Globals.Bounds.X &&
                   mousePosition.Y >= topLeft.Y && mousePosition.Y <= Globals.Bounds.Y)
        {
            //atualiza a posição do ponteiro
            toMouse = InputManager.MousePosition - Position;
            Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);

            if (InputManager.MouseLeftDown) //verifica se o botão esquerdo do rato foi pressionado
            {
                //atira um projetil
                Fire();
            }
            if (InputManager.KeyClicked_R) //verifica se a tecla R foi pressionada
            {
                //recarrega a arma
                Reload();
            }
        }

        base.Update(gameTime);
    }

    public void HandleInput(KeyboardState keyState, GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (keyState.IsKeyDown(Keys.W))
        {
            sPosition.Y -= playerSpeed * deltaTime;
        }
        if (keyState.IsKeyDown(Keys.A))
        {
            sPosition.X -= playerSpeed * deltaTime;
        }
        if (keyState.IsKeyDown(Keys.S))
        {
            sPosition.Y += playerSpeed * deltaTime;
        }
        if (keyState.IsKeyDown(Keys.D))
        {
            sPosition.X += playerSpeed * deltaTime;
        }

        // Clamp position to the game bounds
        sPosition.X = MathHelper.Clamp(sPosition.X, 0, Globals.Bounds.X);
        sPosition.Y = MathHelper.Clamp(sPosition.Y, 0, Globals.Bounds.Y);

        playerRectangle.X = (int)sPosition.X;
        playerRectangle.Y = (int)sPosition.Y;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Draw the player with rotation
        spriteBatch.Draw(
            sTexture,
            sPosition,
            sRectangles[frameIndex],
            Color.White,
            Rotation + MathHelper.PiOver2,
            new Vector2(sRectangles[frameIndex].Width / 2, sRectangles[frameIndex].Height / 2),
            0.18f,
            SpriteEffects.None,
            0f
        );
    }
}
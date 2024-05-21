namespace Awesome_Game;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameManager gameManager;
    private SpriteFont font;
    private bool gameOver = false;
    private Button resetButton;
    private Texture2D buttonTexture;
    private MouseState previousMouseState;
    public static Game1 Instance { get; private set; }
    private bool isFullScreen = false;

    public Game1()
    {
        Instance = this;
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += OnClientSizeChanged;
    }

    protected override void Initialize()
    {
        Globals.Bounds = new(1100, 750);
        _graphics.PreferredBackBufferWidth = Globals.Bounds.X;
        _graphics.PreferredBackBufferHeight = Globals.Bounds.Y;
        _graphics.ApplyChanges();

        Globals.Content = Content;

        gameManager = new GameManager(GraphicsDevice);

        int initialKills = GameStats.Instance.Kills;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.SpriteBatch = _spriteBatch;

        //play background music
        SoundManager.Instance.PlayBackgroundMusic();

        font = Content.Load<SpriteFont>("Fonts/arial");
        buttonTexture = Content.Load<Texture2D>("resetButton");
        resetButton = new Button(buttonTexture, new Vector2(Globals.Bounds.X / 2 - buttonTexture.Width / 2, Globals.Bounds.Y / 2 + 50));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        MouseState mouseState = Mouse.GetState();
        KeyboardState keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.F11))
        {
            ToggleFullScreen();
        }

        if (gameOver)
        {
            resetButton.Update(mouseState);

            if (resetButton.IsClicked && previousMouseState.LeftButton == ButtonState.Released)
            {
                ResetGame();
            }

            previousMouseState = mouseState;
            return;
        }

        Globals.Update(gameTime);
        gameManager.Update(gameTime);
        LevelManager.Instance.GetHarder();

        base.Update(gameTime);
    }

    private void ToggleFullScreen()
    {
        isFullScreen = !isFullScreen;

        if (isFullScreen)
        {
            DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            _graphics.PreferredBackBufferWidth = displayMode.Width;
            _graphics.PreferredBackBufferHeight = displayMode.Height;
            _graphics.IsFullScreen = true;
            Globals.Bounds = new(displayMode.Width, displayMode.Height);
        }
        else
        {
            _graphics.PreferredBackBufferWidth = 1100;
            _graphics.PreferredBackBufferHeight = 750;
            _graphics.IsFullScreen = false;
        }

        _graphics.ApplyChanges();
    }

    private void OnClientSizeChanged(object sender, EventArgs e)
    {
        if (!isFullScreen)
        {
            Globals.Bounds = new(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin();
        gameManager.Draw(gameTime);

        _spriteBatch.DrawString(font, "Kill Count: " + GameStats.Instance.Kills + " - - High Score: " + GameStats.Instance.HighScore, new Vector2(10, 10), Color.Black);

        _spriteBatch.DrawString(font, "Ammo: " + gameManager.Player.Ammo + " / " + gameManager.Player.maxAmmo, new Vector2(5, 60), Color.Black);

        _spriteBatch.DrawString(font, "HP: " + PlayerStats.Instance.player_HP, new Vector2(10, 110), Color.Black);

        if (gameOver)
        {
            _spriteBatch.DrawString(font, "Perdeu", new Vector2(Globals.Bounds.X / 2 - 80, Globals.Bounds.Y / 2), Color.Red);
            resetButton.Draw(_spriteBatch);
        }

        if (gameManager.Player.isReloading)
        {
            _spriteBatch.DrawString(font, "Reloading ...", new Vector2(Globals.Bounds.X / 2 - 80, 2 * Globals.Bounds.Y / 3 + 60), Color.Black);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    public void GameOver()
    {
        gameOver = true;
        GameStats.Instance.UpdateHighScore();
        GameStats.Instance.Kills = 0;
        PlayerStats.Instance.Load_startStats();
        LevelManager.Instance.Load();
    }

    private void ResetGame()
    {
        gameOver = false;
        gameManager = new GameManager(GraphicsDevice);
        Bot1Manager.Bots1.Clear();
    }
}

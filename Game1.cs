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
    //private float gameOverTime = 0f;
    private MouseState previousMouseState;
    public static Game1 Instance { get; private set; }
    public Game1()
    {
        Instance = this;
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

protected override void Initialize()
{
    Globals.Bounds = new(900, 700);
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

        font = Content.Load<SpriteFont>("Fonts/arial");
        buttonTexture = Content.Load<Texture2D>("resetButton");
        resetButton = new Button(buttonTexture, new Vector2(Globals.Bounds.X / 2 - buttonTexture.Width / 2, Globals.Bounds.Y / 2 + 50));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        MouseState mouseState = Mouse.GetState();

         if (gameOver)
        {
            /*gameOverTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (gameOverTime >= 5f)
            {
                ResetGame();
            }*/

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

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Pink);

        _spriteBatch.Begin();
        gameManager.Draw(gameTime);

        _spriteBatch.DrawString(font, "Kill Count: " + GameStats.Instance.Kills, new Vector2(10, 10), Color.Black);

        _spriteBatch.DrawString(font, "Ammo: " + gameManager.Player.Ammo + " / " + gameManager.Player.maxAmmo, new Vector2(5, 60), Color.Black);
        
        _spriteBatch.DrawString(font, "HP: " + gameManager.Player.Hp, new Vector2(10, 110), Color.Black);

        if (gameOver)
        {
            _spriteBatch.DrawString(font, "Perdeu", new Vector2(Globals.Bounds.X / 2-80, Globals.Bounds.Y / 2), Color.Red);
        }

        if (gameManager.Player.isReloading)
        {
            _spriteBatch.DrawString(font, "Reloading ...", new Vector2(Globals.Bounds.X/2-110,  2*Globals.Bounds.Y/3), Color.Black);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    public void GameOver()
    {
        gameOver = true;
        //gameOverTime = 0f;
    }

    private void ResetGame()
    {
        gameOver = false;
        //gameOverTime = 0f;
        gameManager = new GameManager(GraphicsDevice);
    }
}

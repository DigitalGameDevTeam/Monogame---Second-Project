namespace Awesome_Game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameManager gameManager;
        private Map _map;
        private Vector2 _cameraPosition;
        private SpriteFont font;
        private bool gameOver = false;
        private Button resetButton, startButton, exitButton;
        private Texture2D buttonTexture, startButtonTexture, exitButtonTexture;
        private MouseState previousMouseState;
        public static Game1 Instance { get; private set; }
        private bool isFullScreen = false;
        private bool isPaused = false;
        private Button continueButton;
        private Button mainMenuButton;
        private Texture2D continueButtonTexture;
        private Texture2D mainMenuButtonTexture;

        private enum MenuState
        {
            Menu,
            Playing
        }

        private MenuState currentState = MenuState.Menu;

    //temporary message
    private string temporaryMessage = "";
    private float messageTimeRemaining = 0;

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
                Globals.Bounds = new Point(900, 700);
                _graphics.PreferredBackBufferWidth = Globals.Bounds.X;
                _graphics.PreferredBackBufferHeight = Globals.Bounds.Y;
                _graphics.ApplyChanges();

                Globals.Content = Content;

                gameManager = new GameManager(GraphicsDevice);

            // Initialize the map with the desired scale
            float mapScale = 0.05f;
            _map = new Map(GraphicsDevice, new Point(Globals.Bounds.X, Globals.Bounds.Y), mapScale * 4);
            _cameraPosition = Vector2.Zero;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = _spriteBatch;

            // Play background music
            SoundManager.Instance.PlayBackgroundMusic();

            font = Content.Load<SpriteFont>("Fonts/arial");

            // Load button textures
            startButtonTexture = Content.Load<Texture2D>("start");
            exitButtonTexture = Content.Load<Texture2D>("exit");
            buttonTexture = Content.Load<Texture2D>("respawn");
            continueButtonTexture = Content.Load<Texture2D>("continue");
            mainMenuButtonTexture = Content.Load<Texture2D>("back");

            // Calculate button positions
            Vector2 startButtonPosition = new Vector2((Globals.Bounds.X - startButtonTexture.Width) / 2, (Globals.Bounds.Y - startButtonTexture.Height) / 2 - 50);
            Vector2 exitButtonPosition = new Vector2((Globals.Bounds.X - exitButtonTexture.Width) / 2, (Globals.Bounds.Y - exitButtonTexture.Height) / 2 + 50);
            Vector2 resetButtonPosition = new Vector2((Globals.Bounds.X - buttonTexture.Width) / 2, (Globals.Bounds.Y - buttonTexture.Height) / 2 + 150);
            continueButton = new Button(continueButtonTexture, new Vector2(Globals.Bounds.X / 2 - continueButtonTexture.Width / 2, Globals.Bounds.Y / 2));
            mainMenuButton = new Button(mainMenuButtonTexture, new Vector2(Globals.Bounds.X / 2 - mainMenuButtonTexture.Width / 2, Globals.Bounds.Y / 2 + 100));

            // Create buttons
            startButton = new Button(startButtonTexture, startButtonPosition);
            exitButton = new Button(exitButtonTexture, exitButtonPosition);
            resetButton = new Button(buttonTexture, resetButtonPosition);

            // Inside LoadContent method of Game1 class
            startButton.UpdateRectangle(new Vector2((Globals.Bounds.X - startButtonTexture.Width) / 2, (Globals.Bounds.Y - startButtonTexture.Height) / 2 - 50));
            exitButton.UpdateRectangle(new Vector2((Globals.Bounds.X - exitButtonTexture.Width) / 2, (Globals.Bounds.Y - exitButtonTexture.Height) / 2 + 50));
            resetButton.UpdateRectangle(new Vector2((Globals.Bounds.X - buttonTexture.Width) / 2, (Globals.Bounds.Y - buttonTexture.Height) / 2 + 150));

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.F11))
            {
                ToggleFullScreen();
            }

            if (currentState == MenuState.Playing)
            {
                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    // Toggle the pause state only if the game is not already in the main menu state
                    if (!isPaused)
                    {
                        // Pause the game
                        isPaused = true;
                    }
                    else
                    {
                        // Unpause the game
                        isPaused = false;
                    }
                }

                if (isPaused)
                {
                    // Update pause menu buttons
                    continueButton.Update(mouseState);
                    mainMenuButton.Update(mouseState);

                    // Handle button clicks
                    if (continueButton.IsClicked)
                    {
                        isPaused = false;
                        // Additional logic to resume the game
                    }
                    else if (mainMenuButton.IsClicked)
                    {
                        // Return to the main menu
                        ReturnToMainMenu();
                    }
                }
                else
                {
                    // Update game logic when not paused
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
                }
            }
            else
            {
                // Update menu logic when in the main menu state
                startButton.Update(mouseState);
                exitButton.Update(mouseState);

                if (startButton.IsClicked && previousMouseState.LeftButton == ButtonState.Released)
                {
                    currentState = MenuState.Playing;
                }

                if (exitButton.IsClicked && previousMouseState.LeftButton == ButtonState.Released)
                {
                    Exit();
                }
            }

            previousMouseState = mouseState;

            base.Update(gameTime);
    
        //temporary message

        if (messageTimeRemaining > 0)
        {
            messageTimeRemaining -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (messageTimeRemaining <= 0)
            {

                temporaryMessage = "";
            }
        }
        //end of temporary message
    }

    //temporary message
    public void ShowTemporaryMessage(string message, float duration)
    {
        temporaryMessage = message;
        messageTimeRemaining = duration;
    }
        private void ReturnToMainMenu()
        {
            // Reset game state and return to the main menu state
            isPaused = false;
            currentState = MenuState.Menu;

            // Additional logic to reset game state if needed
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

                // Recalculate button positions
                Vector2 startButtonPosition = new Vector2((Globals.Bounds.X - startButtonTexture.Width) / 2, (Globals.Bounds.Y - startButtonTexture.Height) / 2 - 50);
                Vector2 exitButtonPosition = new Vector2((Globals.Bounds.X - exitButtonTexture.Width) / 2, (Globals.Bounds.Y - exitButtonTexture.Height) / 2 + 50);
                Vector2 resetButtonPosition = new Vector2((Globals.Bounds.X - buttonTexture.Width) / 2, (Globals.Bounds.Y - buttonTexture.Height) / 2 + 150);

                startButton.UpdateRectangle(startButtonPosition);
                exitButton.UpdateRectangle(exitButtonPosition);
                resetButton.UpdateRectangle(resetButtonPosition);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            switch (currentState)
            {
                case MenuState.Menu:
                    _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
                    _spriteBatch.DrawString(font, "CYBER ASSAULT", new Vector2((Globals.Bounds.X - font.MeasureString("CYBER ASSAULT").X) / 2, 110), Color.Black);
                    startButton.Draw(_spriteBatch);
                    exitButton.Draw(_spriteBatch);
                    _spriteBatch.End();
                    break;

                case MenuState.Playing:
                    _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                    _map.Draw(_spriteBatch, _cameraPosition);
                    _spriteBatch.End();

                    _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                    gameManager.Draw(gameTime);
                    _spriteBatch.End();

                    _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    _spriteBatch.DrawString(font, "Kill Count: " + GameStats.Instance.Kills + " - - High Score: " + GameStats.Instance.HighScore, new Vector2(10, 10), Color.Black);
                    _spriteBatch.DrawString(font, "Ammo: " + gameManager.Player.Ammo + " / " + gameManager.Player.maxAmmo, new Vector2(5, 60), Color.Black);
                    _spriteBatch.DrawString(font, "HP: " + PlayerStats.Instance.player_HP, new Vector2(10, 110), Color.Black);

                    if (gameOver)
                    {
                        resetButton.Draw(_spriteBatch);
                    }

                    if (gameManager.Player.isReloading)
                    {
                        _spriteBatch.DrawString(font, "Reloading ...", new Vector2(Globals.Bounds.X / 2 - 80, 2 * Globals.Bounds.Y / 3 + 60), Color.Black);
                    }

                    _spriteBatch.End();

                    break;
            }

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            if (isPaused)
            {
                // Draw pause menu buttons
                continueButton.Draw(_spriteBatch);
                mainMenuButton.Draw(_spriteBatch);
            }

        //temporary message

        if (!string.IsNullOrEmpty(temporaryMessage))
        {
            _spriteBatch.DrawString(font, temporaryMessage, new Vector2(10, Globals.Bounds.Y - 45), Color.Black);
        }
        //

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
            BotManager.Bots.Clear();
        }
    }
}
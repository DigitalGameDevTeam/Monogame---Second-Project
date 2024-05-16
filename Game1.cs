﻿namespace Awesome_Game;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameManager _gameManager;
    private SpriteFont font;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

protected override void Initialize()
{
    Globals.Bounds = new(1526, 900);
    _graphics.PreferredBackBufferWidth = Globals.Bounds.X;
    _graphics.PreferredBackBufferHeight = Globals.Bounds.Y;
    _graphics.ApplyChanges();

    Globals.Content = Content;
    _gameManager = new();

    int initialKills = GameStats.Instance.Kills;

    base.Initialize();
}

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.SpriteBatch = _spriteBatch;

        font = Content.Load<SpriteFont>("Fonts/arial");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Globals.Update(gameTime);
        _gameManager.Update();

        base.Update(gameTime);
    }

protected override void Draw(GameTime gameTime)
{
    GraphicsDevice.Clear(Color.Pink);

    _spriteBatch.Begin();
    _gameManager.Draw();

    _spriteBatch.DrawString(font, "Kill Count: " + GameStats.Instance.Kills, new Vector2(10, 10), Color.Black);

    _spriteBatch.End();

    base.Draw(gameTime);
}




}

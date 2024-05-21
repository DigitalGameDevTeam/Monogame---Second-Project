namespace Awesome_Game;

public class GameManager
{
    private readonly Player _player;
    public Player Player => _player;
    SpriteBatch spriteBatch;
    
    public GameManager(GraphicsDevice graphicsDevice)
    {
        ProjectileManager.Init();
        spriteBatch = new SpriteBatch(graphicsDevice);

        _player = new(Globals.Content.Load<Texture2D>("player"));
        _player.LoadContent();
        BotManager.Init();
    }
    public void Update(GameTime gameTime)
    {
        InputManager.Update();
        _player.Update(gameTime);
        BotManager.Update(_player, gameTime);
        ProjectileManager.Update(BotManager.Bots);
    }
    public void Draw(GameTime gameTime)
    {
        spriteBatch.Begin();
        _player.Draw(spriteBatch);
        ProjectileManager.Draw();
        BotManager.Draw(spriteBatch);
        spriteBatch.End();
    }
}
namespace Awesome_Game;

public class GameManager
{
    private readonly Player _player;
    public Player Player => _player;

    ContentManager content;
    SpriteBatch spriteBatch;

    public GameManager(GraphicsDevice graphicsDevice)
    {
        ProjectileManager.Init();
        spriteBatch = new SpriteBatch(graphicsDevice);


        _player = new(Globals.Content.Load<Texture2D>("player"));
        _player.LoadContent(content);
        Bot1Manager.Init();
        //_player.SetBounds(_map.MapSize, _map.TileSize);
    }
    public void Update(GameTime gameTime)
    {
        InputManager.Update();
        _player.Update(gameTime);
        //CalculateTranslation();
        Bot1Manager.Update(_player);
        ProjectileManager.Update(Bot1Manager.Bots1);
    }
    public void Draw(GameTime gameTime)
    {
        spriteBatch.Begin();
        _player.Draw(spriteBatch);
        ProjectileManager.Draw();
        Bot1Manager.Draw();
        spriteBatch.End();
    }
}
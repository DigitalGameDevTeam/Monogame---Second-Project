namespace Awesome_Game;

public class GameManager
{
    private readonly Player _player;
    private readonly Map _map;
    private Matrix _translation;

    public GameManager()
    {
        ProjectileManager.Init();
        _map = new Map();
        _player = new(Globals.Content.Load<Texture2D>("player"));
        _player.SetBounds(_map.MapSize, _map.TileSize);
    }
    private void CalculateTranslation()
    {
        var dx = (Globals.Bounds.X / 2) - _player.Position.X;
        var dy = (Globals.Bounds.Y / 2) - _player.Position.Y;
        _translation = Matrix.CreateTranslation(dx, dy, 0f);
    }
    public void Update()
    {
        InputManager.Update();
        _player.Update();
        CalculateTranslation();
        ProjectileManager.Update();
    }
    public void Draw()
    {
        ProjectileManager.Draw();
        _player.Draw();
        _map.Draw();
    }
}
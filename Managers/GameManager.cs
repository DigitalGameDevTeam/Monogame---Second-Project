namespace Awesome_Game;

public class GameManager
{
    private readonly Player _player;

    public GameManager()
    {
        _player = new(Globals.Content.Load<Texture2D>("player"));
    }
    public void Update()
    {
        InputManager.Update();
        _player.Update();
    }
    public void Draw()
    {
        _player.Draw();
    }
}
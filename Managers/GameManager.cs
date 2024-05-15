using System.Data.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Awesome_Game;

public class GameManager
{
    private readonly Player _player;

    public GameManager()
    {
        _player = new(Globals.Content.Load<Texture2D>("player"), new(200, 200));
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
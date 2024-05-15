using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Awesome_Game;

public class Player : Sprite 
{
    public Player(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
    }

    public void Update()
    {
        if (InputManager.Direction != Vector2.Zero)
        {
            var dir = Vector2.Normalize(InputManager.Direction);
            Position += dir * Speed * Globals.TotalSeconds;
        }

        var toMouse = InputManager.MousePosition - Position;
        Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Map
{
    private Texture2D _backgroundTexture;
    public Point TileSize { get; private set; }
    private Point _screenSize;
    private float _scale;

    public Map(GraphicsDevice graphicsDevice, Point screenSize, float scale = 1.0f)
    {
        _screenSize = screenSize;
        _backgroundTexture = Globals.Content.Load<Texture2D>("tiles");

        _scale = scale;
        TileSize = new Point((int)(_backgroundTexture.Width * _scale), (int)(_backgroundTexture.Height * _scale));
    }

public void Draw(SpriteBatch spriteBatch, Vector2 cameraPosition)
{
    // Calculate the number of tiles to cover the screen and beyond
    int tilesX = (_screenSize.X / TileSize.X) + 4; // Triple width
    int tilesY = (_screenSize.Y / TileSize.Y) + 4; // Triple height

    for (int y = 0; y < tilesY; y++)
    {
        for (int x = 0; x < tilesX; x++)
        {
            spriteBatch.Draw(
                _backgroundTexture,
                new Vector2(x * TileSize.X, y * TileSize.Y),
                null,
                Color.White,
                0f,
                Vector2.Zero,
                _scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}





}

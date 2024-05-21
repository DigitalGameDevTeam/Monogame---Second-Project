namespace Awesome_Game
{
    public class Button
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle;
        private float scale;

        public bool IsClicked { get; private set; }

        public Button(Texture2D texture, Vector2 position, float scale = 1f)
        {
            this.texture = texture;
            this.position = position;
            this.scale = scale;
            UpdateRectangle();
        }

        private void UpdateRectangle()
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width * scale), (int)(texture.Height * scale));
        }

        public void UpdateRectangle(Vector2 newPosition)
        {
            position = newPosition;
            UpdateRectangle();
        }

        public void Update(MouseState mouseState)
        {
            if (rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                IsClicked = true;
            }
            else
            {
                IsClicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
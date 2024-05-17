namespace Awesome_Game
{
    public class Button
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle;

        public bool IsClicked { get; private set; }

        public Button(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
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
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}

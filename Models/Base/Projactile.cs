namespace Awesome_Game
{
    public class Projectile : Sprite
    {
        public Vector2 Direction { get; set; }
        public float Lifespan { get; private set; }

        
        // Remove the gameStats property from here

        public Projectile(Texture2D tex, ProjectileData data) : base(tex, data.Position)
        {
            Speed = data.Speed;
            Rotation = data.Rotation;
            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            Lifespan = data.Lifespan;
            // gameStats = stats; // Remove this line
        }
        
        public void CauseDamage()
        {
            Lifespan = 0;
            
            GameStats.Instance.Kills++; // Access the GameStats singleton instance and increment the kill count
        }
        
        public void Update()
        {
            Position += Direction * Speed * Globals.TotalSeconds;
            Lifespan -= Globals.TotalSeconds;
        }
    }
}

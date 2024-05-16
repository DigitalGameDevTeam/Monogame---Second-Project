public static class ProjectileManager
{
    private static Texture2D _texture;
    public static List<Projectile> Projectiles { get; } = new();

    public static void Init()
    {
        _texture = Globals.Content.Load<Texture2D>("bullet");
    }

    public static void AddProjectile(ProjectileData data)
    {
        Projectiles.Add(new Projectile(_texture, data)); // No need to pass GameStats instance here
    }

    public static void Update(List<Bot1> Bots1)
    {
        foreach (var p in Projectiles)
        {
            p.Update();
            foreach (var z in Bots1)
            {
                if (z.HP <= 0) continue;
                if ((p.Position - z.Position).Length() < 32)
                {
                    z.TakeDamage(1);
                    p.CauseDamage();
                    break;
                }
            }
        }
        Projectiles.RemoveAll((p) => p.Lifespan <= 0);
    }

    public static void Draw()
    {
        foreach (var p in Projectiles)
        {
            p.Draw();
        }
    }
}

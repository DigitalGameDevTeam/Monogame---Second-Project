namespace Awesome_Game;

public class Bot1 : MovingSprite
{
    public int HP { get; private set; }
    public Bot1(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        Speed = LevelManager.Instance.bot1_MovementSpeed;
        HP = LevelManager.Instance.bot1_HP;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
    }

    public void Update(Player player)
    {
        var toPlayer = player.Position - Position;
        Rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

        if (toPlayer.Length() > 4)
        {
            var dir = Vector2.Normalize(toPlayer);
            Position += dir * Speed * Globals.TotalSeconds;
        }

        if (toPlayer.Length() < 32)
        {
            player.TakeDamage(10);
            HP = 0;
        }
    }
}
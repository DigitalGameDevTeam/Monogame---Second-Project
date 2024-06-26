namespace Awesome_Game;

public class PowerUpsManager
{
    private static PowerUpsManager instance;
    public static PowerUpsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PowerUpsManager();
            }
            return instance;
        }
    }
    private Random random = new Random();
    public int random_Number()
    {
        int randomNumber = random.Next(0, 4);
        return randomNumber;
    }
    public void levelUp()
    {
        switch (random_Number())
        {
            case 0:
                PlayerStats.Instance.player_HP = PowerUps.upgrade_HP(PlayerStats.Instance.player_HP);
                Game1.Instance.ShowTemporaryMessage("+ HP", 2.0f);
                break;
            case 1:
                PlayerStats.Instance.player_Speed = PowerUps.upgrade_Speed(PlayerStats.Instance.player_Speed);
                Game1.Instance.ShowTemporaryMessage("+ Speed", 2.0f);
                 break;
            case 2:
                PlayerStats.Instance.player_cooldown = PowerUps.reduce_cooldown(PlayerStats.Instance.player_cooldown);
                Game1.Instance.ShowTemporaryMessage("- Bullet Cooldown", 2.0f);
                break;
            case 3:
                PlayerStats.Instance.player_maxAmmo = PowerUps.upgrade_maxAmmo(PlayerStats.Instance.player_maxAmmo);
                Game1.Instance.ShowTemporaryMessage("+ Ammo", 2.0f);
                break;
            case 4:
                PlayerStats.Instance.player_reloadTime = PowerUps.reduce_reloadTime(PlayerStats.Instance.player_reloadTime);
                Game1.Instance.ShowTemporaryMessage("- Reload Time", 2.0f);
                break;
        }
    }
}

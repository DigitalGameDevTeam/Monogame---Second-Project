namespace Awesome_Game;
public class PlayerStats
{
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get
       {
            if (instance == null)
            {
                instance = new PlayerStats();
            }
            return instance;
        } 
    }
    public int player_HP { get; set; }
    public int player_Speed { get; set; }
    public float player_cooldown { get; set; }
    public int player_maxAmmo { get; set; }
    public float player_reloadTime { get; set; }

    public PlayerStats()
    {
        Load_startStats();
    }

    public void Load_startStats()
    {
        player_HP = 100;
        player_Speed = 300;
        player_cooldown = 0.25f;
        player_maxAmmo = 5;
        player_reloadTime = 2f;
    }

}
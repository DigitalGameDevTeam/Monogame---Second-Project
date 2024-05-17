public class GameStats
{
    private static GameStats _instance;
    public  Player Player { get; set; }
    public static GameStats Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameStats();
            return _instance;
        }
    }

    public int Kills { get; set; }

    private GameStats()
    {
        Kills = 0;
    }
}
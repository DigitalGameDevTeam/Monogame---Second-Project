namespace Awesome_Game;
public class LevelManager
{
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LevelManager();
            }
            return instance;
        }
    }
    //Game status
    public int difficultyLevel { get; set; }
    public int bot1_HP { get; set; }
    public int bot1_MovementSpeed { get; set; }
    public float bot1_SpawnRate { get; set; }

    //Games Status Gaps
    public int bot1_HP_Gap = 1;
    public int bot1_MovementSpeed_Gap = 50;
    public float bot1_SpawnRate_Gap = 0.5f;
    public int level_Gap = 15;

    public LevelManager()
    {
        Load();
    }

    public void GetHarder()
    {
        if (GameStats.Instance.Kills > level_Gap)
        {
        difficultyLevel++;
        bot1_HP += bot1_HP_Gap;
        bot1_MovementSpeed += bot1_MovementSpeed_Gap;
        bot1_SpawnRate -= bot1_SpawnRate_Gap;
        level_Gap += 15;
        }
    }

    public void Load()
    {
        difficultyLevel = 1;
        bot1_HP = 1;
        bot1_MovementSpeed = 100;
        bot1_SpawnRate = 1f;
    }
}
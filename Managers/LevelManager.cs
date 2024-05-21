using System.Diagnostics;

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
    public int bot_HP { get; set; }
    public int bot_MovementSpeed { get; set; }
    public float bot_SpawnRate { get; set; }

    //Games Status Gaps
    public int bot_HP_Gap = 1;
    public int bot_MovementSpeed_Gap = 50;
    public float bot_SpawnRate_Gap = 0.5f;
    public int level_Gap = 15;

    public LevelManager()
    {
        Load();
    }

    public void GetHarder()
    {
        if (GameStats.Instance.Kills > level_Gap)
        {
            Debug.WriteLine($"================================== ");
            difficultyLevel++;
            Debug.WriteLine($"difficulty : {difficultyLevel} ");
            bot_HP += bot_HP_Gap;
            Debug.WriteLine($"bot HP : {bot_HP} ");
            bot_MovementSpeed += bot_MovementSpeed_Gap;
            Debug.WriteLine($"bot Speed : {bot_MovementSpeed} ");
            bot_SpawnRate -= bot_SpawnRate_Gap;
            Debug.WriteLine($"spawn Rate : {bot_SpawnRate} ");
            level_Gap += 15;
            Debug.WriteLine($"gap : {level_Gap} ");
            Debug.WriteLine($"================================== ");
        }
    }

    public void Load()
    {
        difficultyLevel = 1;
        bot_HP = 1;
        bot_MovementSpeed = 100;
        bot_SpawnRate = 1f;
    }
}
using System;
using System.IO;

public class GameStats
{
    private static GameStats _instance;
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
    public int HighScore { get; private set; }

    private const string HighScoreFileName = "highscore.txt"; // File name to store high score

    private GameStats()
    {
        Kills = 0;
        LoadHighScore(); // Load high score when the instance is created
    }

    public void UpdateHighScore()
    {
        if (Kills > HighScore)
        {
            HighScore = Kills;
            SaveHighScore(); // Save high score if it's updated
        }
    }

    private void LoadHighScore()
    {
        if (File.Exists(HighScoreFileName))
        {
            try
            {
                HighScore = Convert.ToInt32(File.ReadAllText(HighScoreFileName));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading high score: " + e.Message);
            }
        }
    }

    private void SaveHighScore()
    {
        try
        {
            File.WriteAllText(HighScoreFileName, HighScore.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error saving high score: " + e.Message);
        }
    }
}

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

    private const string HighScoreFileName = "highscore.txt";

    private GameStats()
    {
        Kills = 0;
        LoadHighScore();
    }

    public void UpdateHighScore()
    {
        if (Kills > HighScore)
        {
            HighScore = Kills;
            SaveHighScore();
            Kills = 0;
        }
        else
        {
            Kills = 0;
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
                Console.WriteLine("Erro ao ler o HighScore: " + e.Message);
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
            Console.WriteLine("Erro ao guardar o HighScore: " + e.Message);
        }
    }
}

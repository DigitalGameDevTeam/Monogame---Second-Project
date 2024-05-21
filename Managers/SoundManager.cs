namespace Awesome_Game;

public class SoundManager
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SoundManager();
            }
            return instance;
        }
    }

    private SoundEffect playerShoot;
    private SoundEffect playerReload;
    private SoundEffect playerDamage;
    private Song backgroundMusic;
    private float effectVolume = 0.1f;

    private SoundManager()
    {
        playerShoot = Globals.Content.Load<SoundEffect>("Sounds/shoot");
        playerReload = Globals.Content.Load<SoundEffect>("Sounds/shoot_reload");
        playerDamage = Globals.Content.Load<SoundEffect>("Sounds/damage");
        backgroundMusic = Globals.Content.Load<Song>("Sounds/background");
    }

    public void PlayPlayerShoot()
    {
        playerShoot.Play(effectVolume, 0.0f, 0.0f);
    }

    public void PlayPlayerReload()
    {
        playerReload.Play(effectVolume, 0.0f, 0.0f);
    }

    public void PlayPlayerDamage()
    {
        playerDamage.Play(effectVolume, 0.0f, 0.0f);
    }

    public void PlayBackgroundMusic()
    {
        MediaPlayer.Volume = 0.1f;
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Play(backgroundMusic);
    }
}
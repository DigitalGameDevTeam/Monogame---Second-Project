using System.Diagnostics;

namespace Awesome_Game;

public static class PowerUps
{
    public static int upgrade_HP(int value)
    {
        value += 20;
        Debug.WriteLine($"  >> upgrade_HP : {value}");
        return value;
    }
    public static int upgrade_Speed(int value)
    {
        value += 50;
        Debug.WriteLine($"  >> upgrade_Speed : {value}");
        return value;
    }
    public static float reduce_cooldown(float value)
    {
        value -= 0.05f;
        Debug.WriteLine($"  >> reduce_cooldown : {value}");
        return value;
    }
    public static int upgrade_maxAmmo(int value)
    {
        value += 2;
        Debug.WriteLine($"  >> upgrade_maxAmmo : {value}");
        return value;
    }
    public static float reduce_reloadTime(float value)
    {
        value -= 0.05f;
        Debug.WriteLine($"  >> reduce_reloadTime : {value}");
        return value;
    }

    /* if(GameStats.Instance.Kills>=5){
               playerSpeed = PowerUps.upgrade_Speed(playerSpeed);

    }*/
}
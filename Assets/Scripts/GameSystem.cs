using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSystem
{
    public enum GamePanelType
    {
        None,
        MainMenu,
        Map,
        Game,
        Pause,
        Lose,
        Won
    }
    public enum SaveValueType
    {
        Coin,
        TempLevel,
        TempWeapon
    }

    public static void SetInt(SaveValueType type, int value)
    {
        PlayerPrefs.SetInt(type.ToString(), value);
    }
    public static int GetInt(SaveValueType type)
    {
        if (PlayerPrefs.HasKey(type.ToString()))
            return PlayerPrefs.GetInt(type.ToString());

        return 0;
    }
}

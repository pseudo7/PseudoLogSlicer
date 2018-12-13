using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour
{
    public static PlayerPrefManager Instance;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public LevelName HighScore
    {
        set
        {
            if (!PlayerPrefs.HasKey(Constants.HIGH_SCORE_KEY))
                PlayerPrefs.SetInt(Constants.HIGH_SCORE_KEY, (int)value);
            if ((int)value > PlayerPrefs.GetInt(Constants.HIGH_SCORE_KEY))
                PlayerPrefs.SetInt(Constants.HIGH_SCORE_KEY, (int)value);
            PlayerPrefs.Save();
        }
        get
        {
            if (!PlayerPrefs.HasKey(Constants.HIGH_SCORE_KEY))
                return LevelName.Level1;
            return (LevelName)PlayerPrefs.GetInt(Constants.HIGH_SCORE_KEY);
        }

    }
    //public string GetNameFromLevel(LevelName levelName)
    //{
    //    return string.Format("Level {0}", (levelName + 1).ToString("0#"));
    //}
}

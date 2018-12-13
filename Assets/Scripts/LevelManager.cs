using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    static Dictionary<LevelName, LevelInfo> levels;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            levels = new Dictionary<LevelName, LevelInfo>
            {
                { LevelName.Level1, new LevelInfo(5, new float[] { 1, -2 } ,1f, 1)},
                { LevelName.Level2, new LevelInfo(7, new float[] { 1, -2, 3 } ,1f, 1)},
                { LevelName.Level3, new LevelInfo(10, new float[] { 1, 3, -2, 4 } ,1.25f, 2)},
                { LevelName.Level4, new LevelInfo(12, new float[] { 1, 2, -2, -3, .5f } ,1.5f, 3)},
                { LevelName.Level5, new LevelInfo(15, new float[] { 1, 3, -2, 4 } ,1.8f, 5)},
            };
        }
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(LevelName levelName)
    {
        Debug.Log(levelName.ToString() + " Loaded");
        Utility.isGameOver = false;
        TrunkRotator.Instance.RemoveBonueses();
        GameManager.Instance.currentLevelName = levelName;
        GameManager.Instance.UpdateLevelInfo(levels[levelName]);
        KnifeStack.Instance.LoadKnives();
        TrunkRotator.Instance.UpdateRotation();
        UIManager.Instance.UpdateLevelName(levelName);
        PlayerPrefManager.Instance.HighScore = levelName;
    }

    public void LoadNextLevel()
    {
        if ((int)GameManager.Instance.currentLevelName < levels.Count)
            LoadLevel((LevelName)((int)GameManager.Instance.currentLevelName + 1));
        else
            Debug.Log("Game FINISHED!!");
    }
}

public enum LevelName
{
    Level1, Level2, Level3, Level4, Level5
}

public struct LevelInfo
{
    public int initKnives;
    public float[] rotationPattern;
    public float trunkScaleMultipler;
    public int bonuses;

    public LevelInfo(int initKnives, float[] rotationPattern, float trunkScaleMultipler, int bonuses)
    {
        this.initKnives = initKnives;
        this.rotationPattern = rotationPattern;
        this.trunkScaleMultipler = trunkScaleMultipler;
        this.bonuses = bonuses;
    }
}

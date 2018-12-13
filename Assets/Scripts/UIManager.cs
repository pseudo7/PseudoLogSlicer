using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject gameoverScreen;

    public Text remainingKnivesText;
    public Text levelNameText;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void UpdateLevelName(LevelName levelName)
    {
        levelNameText.text = string.Format("Level {0}", ((int)levelName + 1).ToString("0#"));
    }

    public void UpdateKnives(int knives)
    {
        remainingKnivesText.text = string.Format("{0}", knives.ToString("0#"));
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }

    public void LoadFirstLevel()
    {
        LevelManager.Instance.LoadLevel(LevelName.Level1);
    }

    public void UpdateScores(Transform screenTransform, bool isWin)
    {
        screenTransform.GetComponent<GameOverScreen>().HighScore = PlayerPrefManager.Instance.HighScore + 1;
        screenTransform.GetComponent<GameOverScreen>().CurrentScore = GameManager.Instance.currentLevelName + 1;
    }
}

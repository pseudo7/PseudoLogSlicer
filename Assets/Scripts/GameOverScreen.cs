using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text highScoreText;
    public Text currentScoreText;

    public LevelName HighScore
    {
        set
        {
            highScoreText.text = string.Format("Highscore\n<color=\"#09B723\">Level {0}</color>", ((int)value).ToString("0#"));
        }
    }

    public LevelName CurrentScore
    {
        set
        {
            currentScoreText.text = string.Format("Current\n<color=\"#BE2342\">Level {0}</color>", ((int)value).ToString("0#"));
        }
    }
}

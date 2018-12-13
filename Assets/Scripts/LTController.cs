using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTController : MonoBehaviour
{

    public static LTController Instance;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public GameObject gameoverScreenWin, gameoverScreenLose;

    public void ShowGameOverScreen(bool win)
    {
        var screenGO = win ? gameoverScreenWin : gameoverScreenLose;
        UIManager.Instance.UpdateScores(screenGO.transform, win);
        if (!LeanTween.isTweening())
            LeanTween.scale(screenGO, Vector3.one, .5f);
    }

    public void HideGameOverScreen(bool win)
    {
        if (!LeanTween.isTweening())
            LeanTween.scale(win ? gameoverScreenWin : gameoverScreenLose, Vector3.zero, .5f);
    }

}

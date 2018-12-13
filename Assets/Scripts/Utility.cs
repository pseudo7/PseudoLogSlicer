using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static int remainingKnives;
    public static int successfulHits;
    public static bool isGameOver;
}
public static class Constants
{
    public const string KNIFE_TAG = "KNIFE";
    public const string BONUS_TAG = "BONUS";
    public const string BLUE_HEX = "0086FF";

    public const string HIGH_SCORE_KEY = "HIGH_SCORE_KEY";
}

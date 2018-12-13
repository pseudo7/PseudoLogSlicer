using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int initKnives;
    public int bonuses;
    public float trunkScaleMultipler;
    public float[] rotationPattern;
    public Transform trunkTransform;
    public float knifeSpeed = 10f;
    public GameObject knifePrefab;
    public Transform remainingKnivesTransform;
    public GameObject brokenTrunk;
    public LevelName currentLevelName = LevelName.Level1;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LevelManager.Instance.LoadLevel(currentLevelName);
    }

    public void UpdateLevelInfo(LevelInfo levelInfo)
    {
        bonuses = levelInfo.bonuses;
        initKnives = levelInfo.initKnives;
        rotationPattern = levelInfo.rotationPattern;
        trunkScaleMultipler = levelInfo.trunkScaleMultipler;
        TrunkRotator.Instance.SpawnBoneses();
        trunkTransform.GetChild(trunkTransform.childCount - 1).localScale = Vector3.one * levelInfo.trunkScaleMultipler;
        trunkTransform.GetChild(trunkTransform.childCount - 1).GetComponent<Trunk>().UpdateTrunkRadius(trunkScaleMultipler);
        UIManager.Instance.UpdateKnives(initKnives);
        Utility.remainingKnives = initKnives;
    }

    public void ThrowKnife(Transform knifeSpawnTransform)
    {
        if (Utility.remainingKnives > 0 && !Utility.isGameOver)
        {
            GameObject knife = Instantiate(knifePrefab, knifeSpawnTransform);
            knife.GetComponent<Rigidbody2D>().AddForce((trunkTransform.position - knife.transform.position) * knifeSpeed, ForceMode2D.Impulse);
            Destroy(remainingKnivesTransform.GetChild(remainingKnivesTransform.childCount - 1).gameObject);
            UIManager.Instance.UpdateKnives(--Utility.remainingKnives);
        }
    }

    public void GameOver(bool isWin)
    {
        PlayerPrefManager.Instance.HighScore = currentLevelName;
        Utility.isGameOver = true;
        Utility.successfulHits = 0;
        if (isWin)
            StartCoroutine(SlowDownTime(true));
        else
            StartCoroutine(SlowDownTime());
    }

    IEnumerator SlowDownTime()
    {
        Handheld.Vibrate();
        //while ((Time.timeScale -= Time.fixedDeltaTime / 2) > 0.1f)
        //    yield return new WaitForEndOfFrame();
        //Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        TrunkRotator.Instance.RemoveStuckKnives();
        KnifeStack.Instance.RemoveAllKnives();

        LTController.Instance.ShowGameOverScreen(false);

        //Time.timeScale = 1;
        //LevelManager.Instance.LoadLevel(LevelName.Level1);
    }

    IEnumerator SlowDownTime(bool loadNextLevel)
    {
        trunkTransform.gameObject.SetActive(false);
        brokenTrunk.SetActive(true);
        //yield return new WaitForSeconds(1);
        //while (Time.timeScale >= 0.2f)
        //{
        //    Time.timeScale -= (Time.fixedDeltaTime / 2);
        //    yield return new WaitForEndOfFrame();
        //}
        //Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);
        //Time.timeScale = 1;
        brokenTrunk.SetActive(false);
        trunkTransform.gameObject.SetActive(true);
        TrunkRotator.Instance.RemoveStuckKnives();
        BrokenTrunk.Instance.ResetBrokenTrunk();
        LTController.Instance.ShowGameOverScreen(true);
        //if (loadNextLevel)
        //    LevelManager.Instance.LoadNextLevel();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrunkRotator : MonoBehaviour
{
    public static TrunkRotator Instance;

    public Transform brokenTrunk;
    public float rotateSpeed = 5f;
    public float delay = 2;

    public Transform spawnPointH, spawnPointW;
    //float spawnOffset;

    //Image img;

    public GameObject[] bonuesesPrefabs;

    float[] pattern;
    float patternPoint;

    static Coroutine rotationCoroutine, patternUpdaterCoroutine;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    //private void Start()
    //{
    //img = transform.GetChild(transform.childCount - 1).GetComponent<Image>();
    //}

    private void OnDisable()
    {
        if (rotationCoroutine != null)
            StopCoroutine(rotationCoroutine);
        if (patternUpdaterCoroutine != null)
            StopCoroutine(patternUpdaterCoroutine);
    }

    public void UpdateRotation()
    {
        if (rotationCoroutine != null)
            StopCoroutine(rotationCoroutine);
        if (patternUpdaterCoroutine != null)
            StopCoroutine(patternUpdaterCoroutine);

        rotationCoroutine = StartCoroutine(TrunkRotation());
        patternUpdaterCoroutine = StartCoroutine(PatternUpdater());
    }

    IEnumerator TrunkRotation()
    {
        while (gameObject.activeInHierarchy)
        {
            transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed * patternPoint);
            brokenTrunk.rotation = transform.rotation;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator PatternUpdater()
    {
        pattern = GameManager.Instance.rotationPattern;
        int patternLength = pattern.Length;
        int patternIndex = 0;
        while (gameObject.activeInHierarchy)
        {
            patternPoint = pattern[patternIndex++ % patternLength];
            yield return new WaitForSeconds(delay);
        }
    }

    public void SpawnBoneses()
    {
        while (GameManager.Instance.bonuses-- > 0)
        {
            //spawnOffset = (spawn.transform.GetChild(0).GetComponent<RectTransform>().rect.height / 2) - (Screen.width / (float)Screen.height) * 5;
            //spawnRadius = img.rectTransform.rect.width / 2;(spawnRadius * GameManager.Instance.trunkScaleMultipler + spawnOffset);
            //Debug.Log(spawnRadius * GameManager.Instance.trunkScaleMultipler + " " + spawnOffset);
            //Debug.Log((transform.GetChild(transform.childCount - 1).GetComponent<CircleCollider2D>().radius));
            var spawn = bonuesesPrefabs[Random.Range(0, bonuesesPrefabs.Length)];
            var spawnPos = new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized * (transform.GetChild(transform.childCount - 1).GetComponent<CircleCollider2D>().radius /*- (spawn.transform.GetChild(0).GetComponent<Image>().rectTransform.rect.height / 3)*/);
            var spawnRotation = Quaternion.LookRotation(Camera.main.transform.forward/*new Vector3(spawnPos.x, spawnPos.y, transform.position.z)*/);
            Instantiate(spawn, spawnPos, spawnRotation, transform.GetChild(transform.childCount - 1));
        }
    }

    public void RemoveBonueses()
    {
        var parent = transform.GetChild(transform.childCount - 1);
        for (int i = 0; i < parent.childCount; i++)
            Destroy(parent.GetChild(i).gameObject);
    }

    public void RemoveStuckKnives()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
            Destroy(transform.GetChild(i).gameObject);
    }

}

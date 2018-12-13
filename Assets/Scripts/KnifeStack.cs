using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeStack : MonoBehaviour
{
    public static KnifeStack Instance;
    public GameObject knifeUIPrefab;
    public Transform knifeSpawnTransform;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void LoadKnives()
    {
        RemoveAllKnives();
        for (int i = 0; i < GameManager.Instance.initKnives; i++)
            Instantiate(knifeUIPrefab, transform);
    }

    public void RemoveAllKnives()
    {
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);

        for (int i = 0; i < knifeSpawnTransform.childCount; i++)
            Destroy(knifeSpawnTransform.GetChild(i).gameObject);
    }
}

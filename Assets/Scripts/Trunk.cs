using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    //Transform wDis, hDis;
    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        //wDis = transform.GetChild(0);
        //hDis = transform.GetChild(1);
    }

    public void UpdateTrunkRadius(float trunkScaleMultipler)
    {
        //GetComponent<CircleCollider2D>().radius = Mathf.Min(Vector3.Distance(transform.position, wDis.position), Vector3.Distance(transform.position, hDis.position)) * trunkScaleMultipler;
        GetComponent<CircleCollider2D>().radius = Mathf.Min(rectTransform.rect.width, rectTransform.rect.height) / 2;// * trunkScaleMultipler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(Constants.KNIFE_TAG) && !collision.collider.GetComponent<Knife>().isKnifeWasted)
            Utility.successfulHits++;
        Debug.Log("Init Knives: " + GameManager.Instance.initKnives + " Success Hits: " + Utility.successfulHits);
        if (Utility.remainingKnives <= 0)
            if (GameManager.Instance.initKnives == Utility.successfulHits)
                GameManager.Instance.GameOver(true);
    }
}

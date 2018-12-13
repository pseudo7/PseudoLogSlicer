using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 50;
    [Range(1, 10)] public float delay = 5;
    Image spritalImage;
    static bool inverseColor;

    private void Start()
    {
        spritalImage = GetComponent<Image>();
        transform.Rotate(0, 0, Random.Range(0f, 360f));
        StartCoroutine(CrossFadeColor());
    }

    IEnumerator CrossFadeColor()
    {
        while (gameObject.activeInHierarchy)
        {
            spritalImage.CrossFadeColor((inverseColor = !inverseColor) ? Color.black : Color.white, delay, true, true);
            yield return new WaitForSecondsRealtime(delay);
        }
    }

    void Update()
    {
        transform.Rotate(0, 0, -Time.deltaTime * rotationSpeed);
    }
}

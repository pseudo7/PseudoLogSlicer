using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [HideInInspector] public bool isKnifeWasted;
    bool hasCollided;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.BONUS_TAG))
            Destroy(collision.transform.parent.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasCollided)
            return;
        hasCollided = true;

        if (!collision.collider.CompareTag(Constants.KNIFE_TAG))
        {
            transform.SetParent(collision.collider.transform.parent);
            transform.SetAsFirstSibling();
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce((transform.position - collision.collider.transform.position) * 10, ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().AddTorque(100, ForceMode2D.Impulse);
            GetComponent<Collider2D>().isTrigger = true;
            GameManager.Instance.GameOver(false);
            isKnifeWasted = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCollision : MonoBehaviour
{
    BoxCollider2D boxCollider2d;
    private void Awake()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BirdOnScreen.instance.SetColck();
        Destroy(gameObject);
    }
    private void destroyItself() {
        Destroy(gameObject);

    }
}

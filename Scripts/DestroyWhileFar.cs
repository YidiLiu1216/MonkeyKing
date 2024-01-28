using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhileFar : MonoBehaviour
{
    // Start is called before the first frame update
    int DestroyDistance = 7;
    GameObject levelPoint;
    void Awake()
    {
        levelPoint = GameObject.Find("LevelPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (levelPoint.transform.position.y - transform.position.y > DestroyDistance) {
            Destroy(gameObject);
        }
    }
}

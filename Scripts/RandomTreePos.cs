using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTreePos : MonoBehaviour
{
    [SerializeField] float TreeUpDownRange;
    private void Awake()
    {
        float randomrange = Random.Range(-TreeUpDownRange, TreeUpDownRange);
        transform.position = new Vector2(transform.position.x, transform.position.y + randomrange);
    }
}

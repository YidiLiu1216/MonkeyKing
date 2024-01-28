using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] FruitData fruitData;
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (fruitData.isSelfDistroy) {
            Invoke("DestroyItself", fruitData.distroySeconds);
        }
        
    }
    private void FixedUpdate()
    {
        if (fruitData.isRotateWhenThrow) {
           transform.RotateAround(transform.position, Vector3.back, 30);
        }
        
    }
    void DestroyItself() {
        Destroy(gameObject);
    }
    
}

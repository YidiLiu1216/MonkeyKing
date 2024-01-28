using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player"&&collision.GetComponent<Rigidbody2D>().velocity.y < -Mathf.Epsilon) {
            collision.GetComponent<PlayerController>().SetGrounded();
        }
    }
}

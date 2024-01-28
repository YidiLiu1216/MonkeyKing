using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBoxControl : MonoBehaviour
{
    [SerializeField] PlayerController parentContorller;
    private void OnTriggerStay2D(Collider2D collision)
    {
        parentContorller.SetGrounded();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutVines : MonoBehaviour
{
    [SerializeField] AudioSource BananaAudio;
    [SerializeField] AudioSource CutVineAudio;
    private void Awake()
    {
        BananaAudio.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Vine") {
            collision.gameObject.GetComponent<GrabVine>().cutProcess();
            CutVineAudio.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdOnScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public static BirdOnScreen instance { get; private set; }
    [SerializeField] float maxclock;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] AudioSource birdCollisionAudio;
    float clock = 0.0f;
    bool clockcount = false;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clockcount) { clock += Time.deltaTime;
            if (clock > maxclock) {
                clockcount = false;
                sprite.enabled = false;
                clock = 0.0f;
            }
        }
    }
    public void SetColck() {
        clock = 0.0f;
        sprite.enabled = true;
        clockcount = true;
        birdCollisionAudio.Play();
    }
}

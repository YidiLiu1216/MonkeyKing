using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeCollision : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem grapCollision;
    [SerializeField] BoxCollider2D boxCollider2d;
    [SerializeField] float boxOpenTime;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float DestroyTime;
    [SerializeField] AudioSource HitLaugh;
    [SerializeField] AudioSource Explosion;
    float boxOpenTimer;
    bool isCountting = true;
    void Start()
    {
        boxOpenTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        boxOpenTimer += Time.deltaTime;
        if (isCountting&&boxOpenTimer > boxOpenTime) {
            boxCollider2d.enabled = true;
            isCountting = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        boxCollider2d.enabled = false;
        if (collision.tag == "Player") {
            collision.gameObject.GetComponent<PlayerController>().changePlayerHealth(-1,false);
            HitLaugh.Play();
        }
       // Debug.Log(gameObject.GetInstanceID());
        grapCollision.Play();
        Explosion.Play();
        sprite.color = new Color(0, 0, 0, 0);
        
        Invoke("destoryItself", DestroyTime);

    }
    public void destoryItself() {
        Destroy(this.gameObject);
    }
}

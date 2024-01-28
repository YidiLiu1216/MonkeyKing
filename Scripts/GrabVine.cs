using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabVine : MonoBehaviour
{
    public Transform grabpivot;
    [SerializeField] VineControl vineControl;
    [SerializeField] List<Sprite> vineSprites;
    [SerializeField] float randomLeftRight;
    [SerializeField] float growTime;
    Vector3 offSet = new Vector3(0, -1.5f, 0);
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2d;
    float growTimer;
    bool isCut=false;
    int spriteID=0;

    public delegate void CutProcess();
    public CutProcess cutProcess;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        spriteID = Random.Range(0, vineSprites.Count);
        spriteRenderer.sprite = vineSprites[spriteID];
        cutProcess += Cut;
        float randomrange = Random.Range(-randomLeftRight,randomLeftRight);
        transform.position = new Vector2(transform.position.x + randomrange, transform.position.y);
        growTimer = 0;
    }
    private void Update()
    {
        if (isCut) {
            growTimer += Time.deltaTime;
            if (growTimer > growTime) {
                GrowBack();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController.isGrounded == false&&playerController.isSwinging==false)
            {
                //playerController.Grab(transform);
                playerController.Grab(grabpivot,offSet);
                vineControl.SetSwinging(true);
                cutProcess += playerController.cutFall;
            }
        }
    }
    public void Cut() {
        boxCollider2d.enabled = false;
        spriteRenderer.enabled = false;
        isCut = true;
    }
    void GrowBack() {
        boxCollider2d.enabled = true;
        spriteRenderer.enabled = true;
        isCut = false;
        growTimer = 0;
    }
}

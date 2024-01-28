using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFruitOnPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] List<Sprite> fruitsprites;
    [SerializeField] List<int> fruitPossibility;
    [SerializeField] List<FruitData> fruitdata;
    [SerializeField] BoxCollider2D boxCollider2D;
    int fruitid = -1;

    void Start()
    {
        SpwanFruit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpwanFruit() {
        if (LevelGenerate.instance.diffculty < 2) {
            return;
        }
        int fruitrange = Random.Range(0, LevelGenerate.instance.diffculty-1);
        fruitrange = Mathf.Clamp(fruitrange,0, 2);
        for (int i = 0; i <= fruitrange; i++)
        {
            int dice = Random.Range(0, 20);
            
            if (dice > fruitPossibility[i])
            {
                //Debug.Log(dice);
                sprite.sprite = fruitsprites[i];
                sprite.color = Color.white;
                fruitid = i;
                boxCollider2D.enabled=true;
                break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            bool ispickup=collision.GetComponent<PlayerController>().PickUpFruit(fruitdata[fruitid]);
            if (ispickup) {
                Destroy(gameObject);
            }
        }
    }
}

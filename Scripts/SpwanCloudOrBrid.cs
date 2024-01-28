using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanCloudOrBrid : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("CloudControl")]
    [SerializeField] List<GameObject> cloudPrefab;
    [SerializeField] float mincloudSpeed;
    [SerializeField] float maxcloudSpeed;
    [SerializeField] int cloudSpawnPosibility;
    [SerializeField] int SpawnCloudPositionRange;
    [SerializeField] float CloudUpDownRandomRange;
    [Header("BirdControl")]
    [SerializeField] GameObject birdPrefab;
    [SerializeField] float minbirdSpeed;
    [SerializeField] float maxbirdSpeed;
    [SerializeField] int birdSpawnPosibility;
    [SerializeField] float SpawnBirdTime;
    [SerializeField] int birdSpawnPosition;
    int difficulty;
    float SpawnBirdTimer;
    void Start()
    {
        difficulty = LevelGenerate.instance.diffculty;
        SpawnBirdTimer = 0;
        if (difficulty >= 4) {
            int i = Random.Range(0, 20);
            if (i > cloudSpawnPosibility) {
                SpawnCloud();
            }
        }
    }
    private void Update()
    {
        
        if (difficulty >= 5)
        {
            SpawnBirdTimer += Time.deltaTime;
            if (SpawnBirdTimer > SpawnBirdTime) {
                SpawnBirdTimer = 0;
                if (Random.Range(0, 20) > birdSpawnPosibility) {
                    SpawnBird();
                }
                
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

    }
    void SpawnBird() {
        float speed = Random.Range(minbirdSpeed, maxbirdSpeed);
        int dir = Random.Range(0, 2);
        float up = Random.Range(1, CloudUpDownRandomRange);
        if (dir == 0)
        {
            GameObject bird = Instantiate(birdPrefab, new Vector2(birdSpawnPosition, transform.position.y + up), Quaternion.identity);
            bird.GetComponent<SpriteRenderer>().flipX = true;
            bird.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        }
        else
        {
            GameObject bird = Instantiate(birdPrefab, new Vector2(-birdSpawnPosition, transform.position.y + up), Quaternion.identity);
            bird.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
    }
    void SpawnCloud (){
        float speed = Random.Range(mincloudSpeed, maxcloudSpeed);
        int pos = Random.Range(0, SpawnCloudPositionRange);
        int dir = Random.Range(0, 2);
        float up = Random.Range(0, CloudUpDownRandomRange);
        int c = Random.Range(0, cloudPrefab.Count);
        if (dir == 0)
        {
            GameObject cloud = Instantiate(cloudPrefab[c], new Vector2(pos, transform.position.y+up), Quaternion.identity);
            cloud.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed,0);
        }
        else {
            GameObject cloud = Instantiate(cloudPrefab[c], new Vector2(-pos, transform.position.y+up), Quaternion.identity);
            cloud.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }

    }
}

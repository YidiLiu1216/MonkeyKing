using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject backgroundEnd;
    [SerializeField] int destoryDistance;
    [SerializeField] GameObject background;
    [SerializeField] Transform levelpoint;
    [SerializeField] int spawnDistance;
    public bool IsInitbackground;
    float endy;
    bool entered = false;
    private void Update()
    {
        if (backgroundEnd.transform.position.y - levelpoint.position.y< spawnDistance) {
            GameObject newlevel=Instantiate(background, new Vector3(0, backgroundEnd.transform.position.y+8, 0), Quaternion.identity);
            backgroundEnd = newlevel.transform.GetChild(0).gameObject;
        }
    }
}

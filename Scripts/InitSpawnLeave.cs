using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSpawnLeave : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> inactiveVines;
    List<GameObject> activeVines=new List<GameObject>();
    void Awake()
    {
        RandomVineNum();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RandomVineNum()
    {
        int initNum = Random.Range(2, 5);
        //Debug.Log(initNum);
        while (initNum > 0)
        {
            if (inactiveVines.Count >= 1)
            {
                int removeIndex = Random.Range(0, inactiveVines.Count);
                //Debug.Log(removeIndex);
                GameObject obj = inactiveVines[removeIndex];
                obj.SetActive(true);
                activeVines.Add(obj);
                inactiveVines.RemoveAt(removeIndex);
        
            }
            initNum -= 1;
        }
    }
}

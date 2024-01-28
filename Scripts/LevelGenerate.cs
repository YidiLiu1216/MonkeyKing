using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    public static LevelGenerate instance { get; private set; }
    [SerializeField] int levelGenerateDistance;
    [SerializeField] Transform levelTransform;
    [SerializeField] List<GameObject> levels;
    [SerializeField] List<int> diffcultyControl;
    [SerializeField] List<int> diffcultyLayers;
    [SerializeField] float levelStep;
    [SerializeField] float levelBoundY=3.5f;
    int maxdiffculty = 5;
    [SerializeField] public int diffculty;
    int heightCount;
    void Awake()
    {
        //diffculty = 1;
        heightCount = 0;

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelBoundY - levelTransform.position.y < levelGenerateDistance) {
            GenerateLevel();
        }
    }
    void GenerateLevel() {
        while (levelBoundY - levelTransform.position.y < levelGenerateDistance) {
            levelBoundY += levelStep;
            int levelNum = Random.Range(0,diffcultyControl[diffculty-1]);
            Debug.Log("somebughere");
            Instantiate(levels[levelNum],new Vector3(0,levelBoundY,0),Quaternion.identity);
            heightCount += 1;
            if (heightCount > diffcultyLayers[diffculty - 1]&&diffculty<maxdiffculty) {
                diffculty += 1;
            }
        }
    }
}

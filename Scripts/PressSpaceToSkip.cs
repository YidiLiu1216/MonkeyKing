using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PressSpaceToSkip : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] VideoPlayer video;
    private void Awake()
    {
        video.Play();
        video.loopPointReached += CheckOver;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3")) {
            SceneManager.LoadScene(5);
        }
    }
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(5);
    }
}

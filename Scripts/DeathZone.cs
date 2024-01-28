using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera MainCamera;
    //[SerializeField]BoxCollider2D boxCollider2d;
    [SerializeField] float offSet;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    Vector2 boxColliderSize;
    void Start()
    {
        float ySize = MainCamera.orthographicSize * 2;
        boxColliderSize = new Vector2(ySize * MainCamera.aspect+offSet, ySize+offSet);
        //boxCollider2d.size = boxColliderSize;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Mathf.Abs(transform.position.x - player1.transform.position.x) > boxColliderSize.x||
            transform.position.y-player1.transform.position.y>boxColliderSize.y) {
            player1.GetComponent<PlayerController>().changePlayerHealth(-1,true);
        }
        if (Mathf.Abs(transform.position.x - player2.transform.position.x) > boxColliderSize.x||
            transform.position.y-player2.transform.position.y>boxColliderSize.y) {
            player2.GetComponent<PlayerController>().changePlayerHealth(-1,true);
        }
        */
    }
  
}

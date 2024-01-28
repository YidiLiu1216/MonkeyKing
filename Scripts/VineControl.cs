using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineControl : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("SwingControl")]
    [SerializeField] int swingAngleRange;
    [SerializeField] float swingAngleChangeSpeed;
    [SerializeField] float swingForce;
    public Transform pivot;

    //about swing
    bool isSwinging=false;
    float swingAngle= 0;
    int swingAngleChangeDirection=1;
    Quaternion initRotationVine;
    private void Awake()
    {
        
    }
    void Start()
    {
        initRotationVine= transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSwinging) {
            //Debug.Log("swinging");
            swingAngle += swingAngleChangeSpeed * swingAngleChangeDirection;

            if (swingAngle > swingAngleRange / 2 || swingAngle < -swingAngleRange / 2)
            {
                swingAngleChangeDirection *= -1;
            }
            gameObject.transform.RotateAround(pivot.position, Vector3.back, swingAngleChangeSpeed*swingAngleChangeDirection);

        }
    }
    public float SetSwinging(bool isswing) {
        float returnval;
        if (isswing)
        {
            isSwinging = true;
        }
        else {
            isSwinging = false;
            transform.rotation = initRotationVine;
            
            
            //Vector3 newRotation = new Vector3(0, 10, 0);
            //transform.eulerAngles = newRotation;
        }
        //Debug.Log(swingAngle);
        //Debug.Log(swingAngleChangeDirection);
        if (swingAngle >= 0) {
            if (swingAngleChangeDirection >= 0)
            {
                returnval = 180 - swingAngle;
            }
            else {
                returnval = -swingAngle;
                //return -swingAngle;
            }
        }
        else
        {
            if (swingAngleChangeDirection <= 0)
            {
                returnval = -swingAngle;
                //return -swingAngle;
            }
            else
            {
                returnval = -swingAngle - 180;
                
            }
        }
        swingAngle = 0;
        swingAngleChangeDirection = 1;
        return returnval;
    }
   
}

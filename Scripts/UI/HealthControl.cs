using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
{
    // Start is called before the first frame update
    public static HealthControl instance { get; private set; }
    public Image P1mask;
    public Image P2mask;
    float originalSize1;
    float originalSize2;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        originalSize1 = P1mask.rectTransform.rect.width;
        originalSize2 = P2mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetP1Value(float value)
    {
        P1mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize1 * value);
    }
    public void SetP2Value(float value)
    {
        P2mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize2 * value);
    }
}

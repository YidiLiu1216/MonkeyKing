using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControl : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIControl instance { get; private set; }
    [SerializeField] List<Image> FruitsP1;
    [SerializeField] List<TextMeshProUGUI> FruitNumP1;
    int activeFruitP1 = 0;
    [SerializeField] List<Image> FruitsP2;
    [SerializeField] List<TextMeshProUGUI> FruitNumP2;
    [SerializeField] Color inactiveColor;
    int activeFruitP2 = 0;
    [SerializeField] AudioSource ClickAudio;
    [SerializeField] AudioSource PassAudio;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SwitchFruitUIP1() {
        Debug.Log(activeFruitP1);
        FruitsP1[activeFruitP1].color = inactiveColor;
        activeFruitP1 += 1;
        if (activeFruitP1 >= FruitsP1.Count) {
            activeFruitP1 = 0;
        }
        FruitsP1[activeFruitP1].color = Color.white;
    }
    public void SwitchFruitUIP2() {
        FruitsP2[activeFruitP2].color = inactiveColor;
        activeFruitP2 += 1;
        if (activeFruitP2 >= FruitsP2.Count)
        {
            activeFruitP2 = 0;
        }
        FruitsP2[activeFruitP1].color = Color.white;
    }
    public void SetFruitValue(int playerid,int fruitid,int value) {
        if (playerid == 1)
        {
            FruitNumP1[fruitid].text = value.ToString();
        }
        else {
            FruitNumP2[fruitid].text = value.ToString();
        }
    }
    public void PlayClickSound() {
        ClickAudio.Play();
    }
    public void PlayPassSound() {
        PassAudio.Play();
    }

}

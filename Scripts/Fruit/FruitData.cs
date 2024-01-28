using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Fruit")]
public class FruitData : ScriptableObject
{
    public int id;
    public bool isRotateWhenThrow=false;
    public bool isSelfDistroy=false;
    public int distroySeconds;
    public bool isThrowable = false;
    public bool addHealth = false;
    public int maxHold;
    public int initHold;
}

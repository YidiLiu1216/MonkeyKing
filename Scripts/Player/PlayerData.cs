using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player")]
public class PlayerData : ScriptableObject
{
    public playerid id;
    public string jumpButton;
    public string switchButton;
    public string throwButton;
    public enum playerid {
        P1,
        P2
    }
}

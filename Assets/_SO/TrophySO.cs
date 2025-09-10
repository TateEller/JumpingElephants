using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trophy", menuName = "ScriptableObjects/Trophy", order = 3)]

public class TrophySO : ScriptableObject
{
    public string trophyName;
    public Sprite sprite;
    public int trophieCost;
    public string trophyDesc;
}

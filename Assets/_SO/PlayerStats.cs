using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public int lives = 0;

    public int level = 0;
    internal int lastLevelInt = 0;

    public int coins = 0;

    public List<TrophySO> trophies = new List<TrophySO>();

    public static PlayerStats Instance;
    private void OnEnable()
    {
        Instance = this;
    }

}

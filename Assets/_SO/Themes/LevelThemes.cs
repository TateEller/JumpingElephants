using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelName", menuName = "ScriptableObjects/LevelTheme", order = 2)]
public class LevelThemes : ScriptableObject
{
    public Sprite groundSprite;
    public Sprite backgroundSprite;
    public Sprite wallObj1Sprite;
    public Sprite wallObj2Sprite;
    public GameObject trapPrefab;
}

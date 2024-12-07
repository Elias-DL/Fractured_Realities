using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    // flashlight eigenschappen , probleem -> respawned niet altijd en nooit met prefab
    public int id;
    public string itemName;
    public Sprite icon;
    public GameObject FlashlightPrefab;
}

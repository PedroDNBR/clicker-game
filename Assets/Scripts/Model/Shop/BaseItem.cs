using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/BaseItem")]
public class BaseItem : ScriptableObject
{
    [Header("Item info")]

    public string itemName;

    public Sprite sprite;

    public int price = 2;

}

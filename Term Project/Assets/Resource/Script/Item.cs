using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "아이템 생성")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Equipment,
        Used
    }
    public ItemType itemType;

    public string itemName;
}

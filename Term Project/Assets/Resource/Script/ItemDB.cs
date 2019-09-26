using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    
    public void GetItem(ItemPickUp _itemPickUp)
    {
        if(_itemPickUp != null)
        {
            if(_itemPickUp.item.itemType == Item.ItemType.Equipment)
            {
                //장비 아이템을 얻었을 때 착용한다.
                _itemPickUp.transform.parent = GameManager.instance.player.transform;
                if (GameManager.instance.player.GetComponent<SpriteRenderer>().flipX)
                    _itemPickUp.GetComponent<WeaponController>().LeftChange();
                else
                    _itemPickUp.GetComponent<WeaponController>().RightChange();
            }
            else if(_itemPickUp.item.itemType == Item.ItemType.Used)
            {
                //포션 같은 소모성 아이템을 얻었을 때
            }
        }
    }
}

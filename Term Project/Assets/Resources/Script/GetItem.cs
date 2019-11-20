using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    public ItemDB itemDB;

    //무기를 착용중인지
    public bool isWeapon;

    //방어구를 착용중인지
    public bool isShield;

    public WeaponController currentWeapon;
    public WeaponController currentShield;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if(collision.tag == "Item")
        {
            if (gameObject.layer == 11)
                return;

            GameObject newItem = collision.gameObject;

            itemDB.GetItem( newItem.GetComponent<ItemPickUp>() );

            if (newItem.GetComponent<ItemPickUp>().item.itemType == Item.ItemType.Equipment)
            {
                if(newItem.GetComponent<WeaponController>().weaponType == WeaponController.WeaponType.Shield)
                {
                    currentShield = newItem.GetComponent<WeaponController>();
                    isShield = true;
                }
                else
                {
                    currentWeapon = newItem.GetComponent<WeaponController>();
                    isWeapon = true;
                }
            }
        }
    }
}

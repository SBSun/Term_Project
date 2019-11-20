using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponController
{
    private void OnTriggerStay2D( Collider2D collision )
    {
        if(collision.tag == "Monster")
        {
            Monster monster = collision.GetComponent<Monster>();

            if (GameManager.instance.player.gameObject.layer == 11 || monster.gameObject.layer == 12)
                return;

            if (durability <= 0)
                return;
            
            monster.OnDamage();
            durability--;

            if(durability <= 0)
            {
                GameManager.instance.player.GetComponent<GetItem>().isWeapon = false;
                GameManager.instance.player.GetComponent<GetItem>().currentWeapon = null;
                Destroy( gameObject );
            }
        }
    }
}

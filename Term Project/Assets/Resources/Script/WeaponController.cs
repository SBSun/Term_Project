using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public enum WeaponType
    {
        Sword,
        Gun,
        Shield
    }
    public WeaponType weaponType;

    public int durability; //내구도

    //캐릭터가 왼쪽을 바라보고 있을 때 장비의 위치
    public Vector2 leftPosition;
    public Vector2 rightPosition;
    //캐릭터가 왼쪽을 바라보고 있을 때 장비의 각도
    public float leftRotationZ;
    public float rightRotationZ;

    public void LeftChange()
    {
        transform.localPosition = leftPosition;
        transform.localEulerAngles = new Vector3( 0, 0, leftRotationZ );
    }

    public void RightChange()
    {
        transform.localPosition = rightPosition;
        transform.localEulerAngles = new Vector3( 0, 0, rightRotationZ );
    }
}

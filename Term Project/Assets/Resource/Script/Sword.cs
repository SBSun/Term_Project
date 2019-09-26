using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float leftPositionX;
    public float rightPositionX;
    public float leftRotationZ;
    public float rightRotationZ;

    public void LeftPosition()
    {
        transform.localPosition = new Vector2( leftPositionX, 0 );
        transform.eulerAngles = new Vector3( 0, 0, leftRotationZ );
    }

    public void RightPosition()
    {
        transform.localPosition = new Vector2( rightPositionX, 0 );
        transform.eulerAngles = new Vector3( 0, 0, rightRotationZ );
    }
}

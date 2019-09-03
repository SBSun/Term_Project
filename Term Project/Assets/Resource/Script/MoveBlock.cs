using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    public enum MoveBlockType
    {
        MoveX,
        MoveY
    }
    public MoveBlockType moveBlockType;

    public float maxPositionX;
    public float minPositionX;
    public float maxPositionY;
    public float minPositionY;

    public int   speed;

    public int   direction;

    void Update()
    {
        if (GameManager.instance.isGameover)
            return;

        if (moveBlockType == MoveBlockType.MoveX)
        {
            if (transform.position.x >= maxPositionX)
            {
                direction = -1;
            }
                
            else if (transform.position.x <= minPositionX)
            {
                direction = 1;
            }
                
            Vector3 pos = Vector3.right * direction * speed * Time.deltaTime;
            transform.position = transform.position + pos;
        }
        else
        {
            if (transform.position.y >= maxPositionY)
                direction = -1;

            else if (transform.position.y <= minPositionY)
                direction = 1;

            Vector3 pos = Vector3.up * direction * speed * Time.deltaTime;
            transform.position = transform.position + pos;
        }
    }
}

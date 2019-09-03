using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothWheel : Monster
{
    public enum MoveType
    {
        MoveX,
        MoveY
    }
    public MoveType moveType;

    public float    maxPositionX;
    public float    minPositionX;
    public float    maxPositionY;
    public float    minPositionY;
    public int      direction; //어느 방향부터 시작할껀지
    public float    speed;

    void Update()
    {
        if (GameManager.instance.isGameover)
            return;

        Move();
    }

    public void Move()
    { 
        if(moveType == MoveType.MoveX)
        {
            if (transform.position.x >= maxPositionX)
                direction = -1;

            else if (transform.position.x <= minPositionX)
                direction = 1;

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

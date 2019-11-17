using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Monster
{
    public float maxPositionX;
    public float minPositionX;

    public int direction; //어느 방향부터 시작할껀지
    public float speed;
    private Vector3 pos;

    private RaycastHit hitinfo;

    protected override void Awake()
    {
        base.Awake();

        onDie += () => Destroy( gameObject, 1f );
    }

    void Start()
    {
        
    }


    void Update()
    {
        if (status == Status.Die)
            return;


    }

    public void Move()
    {
        if (transform.localPosition.x >= maxPositionX)
            direction = 1;

        else if (transform.localPosition.x <= minPositionX)
            direction = -1;

        pos = Vector3.right * direction * speed * Time.deltaTime;

        transform.localPosition = transform.localPosition + pos;
    }


}

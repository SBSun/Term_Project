using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Monster
{
    private Vector2 rayDirection;

    public int direction; //어느 방향부터 시작할껀지
    public float speed;
    private int nextMove;

    private Vector3 pos;

    private RaycastHit2D targetHitInfo; //플레이어를 감지하는 Ray
    private RaycastHit2D hitinfo;  //벽을 감지하는 Ray
    private RaycastHit2D hitinfo2;  //땅을 감지하는 Ray

    private Coroutine coroutine;

    public bool isTarget = false; //자기 앞에 플레이어가 있는지 

    protected override void Awake()
    {
        base.Awake();

        onDie += () => Destroy( gameObject, 1f );
    }

    void Start()
    {
        if (!spriteRenderer.flipX)
            rayDirection = Vector2.right;
        else
            rayDirection = Vector2.left;

        coroutine = StartCoroutine( ThinkCoroutine() );
    }


    void Update()
    {
        if (status == Status.Die)
            return;

        TargetCheck();
        Move();
        FrontCheck();
    }

    public void Move()
    {
        pos = Vector3.right * direction * speed * Time.deltaTime;

        transform.localPosition = transform.localPosition + pos;
    }

    public void TargetCheck()
    {
        if(!isTarget)
        {
            targetHitInfo = Physics2D.Raycast( transform.position, rayDirection, 5f, LayerMask.GetMask( "Player" ) );
            Debug.DrawRay( transform.position, new Vector3( rayDirection.x * 5, rayDirection.y, 0 ), Color.black, 0.1f );

            if (targetHitInfo.collider != null)
            {
                isTarget = true;
                StopCoroutine( coroutine );
                speed = 10f;
            }
        }
    }

    public void FrontCheck()
    {
        hitinfo = Physics2D.Raycast( transform.position, rayDirection, 1f, LayerMask.GetMask( "Platform" ) );
        Debug.DrawRay( transform.position, new Vector3( rayDirection.x, rayDirection.y, 0 ), Color.red, 0.1f );

        if(hitinfo.collider != null)
        {
            if(isTarget)
            {
                isTarget = false; //벽에 부딪히면 false
                coroutine = StartCoroutine( ThinkCoroutine() );
            }
            speed = 3f;
            Debug.Log( "앞에 벽" );
            Turn();
        }
        else
        {
            if(!isTarget)
            {
                hitinfo2 = Physics2D.Raycast( new Vector2( transform.position.x + rayDirection.x, transform.position.y ), Vector2.down, 1f, LayerMask.GetMask( "Platform" ) );
                Debug.DrawRay( new Vector2( transform.position.x + rayDirection.x, transform.position.y ), Vector2.down, Color.blue, 0.1f );
                if (hitinfo2.collider == null)
                {
                    Turn();
                }
            }
        }
    }

    public void Turn()
    {
        if (direction == 1)
        {
            direction = -1;
            spriteRenderer.flipX = true;
            rayDirection = Vector2.left;
        }
        else
        {
            direction = 1;
            spriteRenderer.flipX = false;
            rayDirection = Vector2.right;
        }
    }

    private IEnumerator ThinkCoroutine()
    {
        while(status == Status.Live)
        {
            nextMove = Random.Range( 0,2 );
            Debug.Log( "코루틴 실행중" );
            if (nextMove == 0)
            {
                spriteRenderer.flipX = true;
                direction = -1;
                rayDirection = Vector2.left;
            }
            else
            {
                spriteRenderer.flipX = false;
                direction = 1;
                rayDirection = Vector2.right;
            }

            float nextThinkTime = Random.Range( 2f, 4f );

            yield return new WaitForSeconds( nextThinkTime );
        }
    }

}

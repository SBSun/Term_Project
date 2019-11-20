using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    private Vector2 frontVec;

    private int nextMove;

    protected override void Awake()
    {
        base.Awake();
        Think();

        onDie += () => monsterAnimator.SetTrigger( "Death" );
        onDie += () => gameObject.layer = 12;
        onDie += () => Destroy( gameObject, 1f );
    }

    void FixedUpdate()
    {
        if (status == Status.Die)
        {
            monsterRb.velocity = Vector2.zero;
            return;
        }

        monsterRb.velocity = new Vector2( nextMove, monsterRb.velocity.y );
        FrontCheck();
    }

    private void Think() //코루틴으로 바꿀 예정
    {
        nextMove = Random.Range( -1, 2 );

        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        float nextThinkTime = Random.Range( 2f, 5f );
        Invoke( "Think", nextThinkTime );
    }

    public void FrontCheck()
    {
        frontVec = new Vector2( monsterRb.position.x + nextMove * 0.5f, monsterRb.position.y );

        Debug.DrawRay( frontVec, Vector3.down, new Color( 0, 1, 0 ) );

        RaycastHit2D hitInfo = Physics2D.Raycast( frontVec, Vector2.down, 1f, LayerMask.GetMask( "Platform" ) );

        if(hitInfo.collider == null)
        {
            Turn();
        }
    }

    public void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Think() ;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : LivingEntity
{
    [HideInInspector]
    public Rigidbody2D          playerRb;
    private SpriteRenderer      spriteRenderer;
    private Animator            playerAnimator;
    private RaycastHit2D        hitInfo;
    public  Vector3             respawnPosition;

    private float               currentSpeed;
    public float                walkSpeed;

    public float                jumpPower;

    public float                maxPositionX;
    public float                minPositionX;

    private float               moveX;

    public bool                 isGround = false;

    private bool                isMoveBlock = false; //움직이는 블럭 위에 있는지
    private int                 moveBlockSpeed;
    private int                 moveBlockDirection;

    protected override void Awake()
    {
        base.Awake();
        currentSpeed = walkSpeed;
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (status == Status.Die)
            return;

        TryJump();

        if(Input.GetButtonUp("Horizontal"))
            playerRb.velocity = new Vector2( 0, playerRb.velocity.y );

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            playerRb.velocity = new Vector2( 0, playerRb.velocity.y );

        if (moveX != 0)
            playerAnimator.SetBool( "isWalk", true );
        else
            playerAnimator.SetBool( "isWalk", false );

        if (isMoveBlock && moveX == 0)
            MoveBlock();
    }

    void FixedUpdate()
    {
        if (status == Status.Die)
            return;

        Move();
    }

    public void Move()
    {
        moveX = Input.GetAxisRaw( "Horizontal" );

        if (moveX == 1)
            spriteRenderer.flipX = false;
        else if (moveX == -1)
            spriteRenderer.flipX = true;

        playerRb.AddForce( Vector2.right * moveX, ForceMode2D.Impulse );

        if (playerRb.velocity.x > currentSpeed)
            playerRb.velocity = new Vector2( currentSpeed, playerRb.velocity.y );
        else if (playerRb.velocity.x < -currentSpeed)
            playerRb.velocity = new Vector2( -currentSpeed, playerRb.velocity.y );

        CameraCheck();
    }

    public void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
            isGround = false;
            playerAnimator.SetBool( "isGround", isGround );
        }
    }

    public void Jump()
    {
        playerRb.AddForce( Vector2.up * jumpPower, ForceMode2D.Impulse );
    }

    public void MoveBlock() //MoveBlock위에서의 캐릭터 움직임
    {

        Vector3 pos = Vector3.right * moveBlockDirection * moveBlockSpeed * Time.deltaTime;
        transform.position = transform.position + pos;
    }

    public void CameraCheck()
    {
        if (transform.localPosition.x > maxPositionX || transform.localPosition.x < minPositionX)
        {
            Camera.main.GetComponent<CameraFollow>().StopCamera();
        }

        else if (transform.localPosition.x < maxPositionX && transform.localPosition.x > minPositionX)
        {
            Camera.main.GetComponent<CameraFollow>().MoveCamera();
        }
    }

    public override void OnDamage()
    {
        base.OnDamage();
        Debug.Log( "OnDamage" );
        playerRb.velocity = Vector2.zero;

        gameObject.layer = 11;

        spriteRenderer.color = new Color( 1, 1, 1, 0.4f );

        Invoke( "OffDamage", 1.5f );
    }

    public void OffDamage()
    {
        gameObject.layer = 10;

        spriteRenderer.color = new Color( 1, 1, 1, 1 );
    }

    void OnCollisionEnter2D( Collision2D collision )
    {
        if (collision.transform.tag == "Platform" || collision.transform.tag == "MoveBlock")
        {
            if (collision.contacts[0].normal.y > 0.7f)
            {
                isGround = true;
                playerAnimator.SetBool( "isGround", isGround);
                playerRb.velocity = Vector2.zero;
            }

            if(collision.transform.tag == "MoveBlock")
            {
                isMoveBlock = true;
                moveBlockDirection = collision.transform.GetComponent<MoveBlock>().direction;
                moveBlockSpeed = collision.transform.GetComponent<MoveBlock>().speed;
            }
        }
        else if(collision.transform.tag == "Monster")
        {
            Monster monster = collision.transform.GetComponent<Monster>();

            if(monster.monsterType == Monster.MonsterType.RandomMove || monster.monsterType == Monster.MonsterType.Follow)
            {
                if (collision.contacts[0].normal.y > 0.7f)
                {
                    collision.transform.GetComponent<LivingEntity>().OnDamage();
                    playerRb.velocity = new Vector2( 0, 0 );
                    playerRb.AddForce( Vector2.up * 10f, ForceMode2D.Impulse );
                    return;
                }
            }
            OnDamage();
            int direction = transform.position.x - collision.transform.position.x > 0 ? 1 : -1;
            playerRb.AddForce( new Vector2( direction, 1 ) * 5, ForceMode2D.Impulse );
        }
        else if(collision.transform.tag == "DeathLine")
        {
            PlayerRespawn();
        }
    }

    void OnCollisionStay2D( Collision2D collision )
    {
        if (collision.transform.tag == "Platform")
        {
            if (collision.contacts[0].normal.y > 0.6f && collision.contacts[0].normal.y < 0.8f)
            {
                if(moveX == 0)
                {
                    playerRb.velocity = new Vector2( 0, playerRb.velocity.y );
                }
            }
        }
        else if(collision.transform.tag == "MoveBlock")
        {
            moveBlockDirection = collision.transform.GetComponent<MoveBlock>().direction;
            moveBlockSpeed = collision.transform.GetComponent<MoveBlock>().speed;
        }
    }

    void OnCollisionExit2D( Collision2D collision )
    {
        if(collision.transform.tag == "Platform")
        {
            isGround = false;
        }
        else if(collision.transform.tag == "MoveBlock")
        {
            isMoveBlock = false;
        }
    }

    public void PlayerRespawn()
    {
        transform.position = respawnPosition;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : LivingEntity
{
    [HideInInspector]
    public Rigidbody2D          playerRb;
    private SpriteRenderer      spriteRenderer;
    private Animator            playerAnimator;
    private GetItem             getItem;    
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

    private RaycastHit2D        hitInfo;
    public Vector2              boxSize;

    protected override void Awake()
    {
        base.Awake();
        currentSpeed = walkSpeed;
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        getItem = GetComponent<GetItem>();
    }

    void Update()
    {
        if (status == Status.Die)
            return;

        hitInfo = Physics2D.BoxCast( transform.position - new Vector3( 0, spriteRenderer.bounds.extents.y + boxSize.y / 2, 0 ), boxSize, 0, Vector2.down, boxSize.y );

        if (hitInfo.transform != null)
        {
            if (hitInfo.transform.tag == "Platform" || hitInfo.transform.tag == "MoveBlock" || hitInfo.transform.tag == "Player")
            {
                Debug.Log( "땅" );
                isGround = true;
            }
            else
            {
                Debug.Log( hitInfo.transform.name );
                Debug.Log( "땅이 아님" );
                isGround = false;
            }
        }
        else
        {
            isGround = false;
            Debug.Log( "공중" );
        }

        TryJump();

        playerAnimator.SetBool( "isGround", isGround );

        if (Input.GetButtonUp("Horizontal"))
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
        {
            spriteRenderer.flipX = false;

            if(getItem.isWeapon)
                getItem.currentWeapon.RightChange();

            if (getItem.isShield)
                getItem.currentShield.RightChange();
        }
        else if (moveX == -1)
        {
            spriteRenderer.flipX = true;
            if (getItem.isWeapon)
                getItem.currentWeapon.LeftChange();
            
            if (getItem.isShield)
                getItem.currentShield.LeftChange();
        }

        playerRb.AddForce( Vector2.right * moveX, ForceMode2D.Impulse );

        if (playerRb.velocity.x > currentSpeed)
            playerRb.velocity = new Vector2( currentSpeed, playerRb.velocity.y );
        else if (playerRb.velocity.x < -currentSpeed)
            playerRb.velocity = new Vector2( -currentSpeed, playerRb.velocity.y );
    }

    public void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
            isGround = false;
        }       
        if(Input.GetKeyUp(KeyCode.Space) && playerRb.velocity.y > 0)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x ,playerRb.velocity.y * 0.3f);
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

    public override void OnDamage()
    {
        if (!getItem.isShield)
            curLife--;
        else
        {
            getItem.isShield = false;
            Destroy( getItem.currentShield.gameObject );
            getItem.currentShield = null;
        }

        if(curLife <= 0)
        {
            Die();
        }

        gameObject.layer = 11;
        UIManager.instance.stageUI.UpdateHpText();
        playerRb.velocity = Vector2.zero;

        if(getItem.isWeapon)
            getItem.currentWeapon.GetComponent<SpriteRenderer>().color = new Color( 1, 1, 1, 0.4f );
        spriteRenderer.color = new Color( 1, 1, 1, 0.4f );

        Invoke( "OffDamage", 1.5f );
    }

    public void OffDamage()
    {
        gameObject.layer = 10;

        if(getItem.isWeapon)
            getItem.currentWeapon.GetComponent<SpriteRenderer>().color = new Color( 1, 1, 1, 1 );
        spriteRenderer.color = new Color( 1, 1, 1, 1 );
    }

    void OnCollisionEnter2D( Collision2D collision )
    {
        if (collision.transform.tag == "Platform" || collision.transform.tag == "MoveBlock")
        {
            /*if (collision.contacts[0].normal.y > 0.7f)
            {
                isGround = true;
                playerAnimator.SetBool( "isGround", isGround);
                playerRb.velocity = Vector2.zero;
            }*/

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
                if(!isGround)
                {
                    if (transform.localPosition.y > monster.transform.localPosition.y)
                    {
                        if (collision.contacts[0].normal.y > 0.7f)
                        {
                            Debug.Log( "공격" );
                            collision.transform.GetComponent<LivingEntity>().OnDamage();
                            playerRb.velocity = new Vector2( 0, 0 );
                            playerRb.AddForce( Vector2.up * 10f, ForceMode2D.Impulse );
                            return;
                        }
                    }
                }
            }//OnDamage를 먼저 실행시키게 해야한다.
            OnDamage();
            int direction = transform.position.x - collision.transform.position.x > 0 ? 1 : -1;
            playerRb.AddForce( new Vector2( direction, 1 ) * 5, ForceMode2D.Impulse );
        }
        else if(collision.transform.tag == "DeathLine")
        {
            PlayerRespawn();
        }
        else if(collision.transform.tag == "Key")
        {
            GameManager.instance.key += 1;
            SoundManager.instance.PlaySFX("getCoin");
            Destroy(collision.gameObject);
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
        if (collision.transform.tag == "MoveBlock")
        {
            isMoveBlock = false;
        }
    }

    void OnTriggerStay2D( Collider2D collision )
    {
        if(collision.tag == "StartDoor")
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                GameManager.instance.currentStage = GameManager.instance.stages[collision.GetComponent<StartDoor>().chapterNumber];
                GameManager.instance.StageStart();
            }
        }
    }

    public void PlayerRespawn()
    {
        transform.localPosition = respawnPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube( transform.position - new Vector3( 0, spriteRenderer.bounds.extents.y + boxSize.y/2, 0 ), boxSize );
    }
}

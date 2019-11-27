using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryCloud : Monster
{

    public GameObject go_Lightning;
    private RaycastHit2D hitInfo;
    private BoxCollider2D boxCollider;

    public float attackDelay;
    private bool isTarget = false;

    protected override void Awake()
    {
        base.Awake();

        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StartCoroutine( LightningCoroutine() );
    }

    private void Update()
    {
        if (GameManager.instance.isGameover)
            return;

        hitInfo = Physics2D.BoxCast( new Vector2(transform.position.x, transform.position.y - 1.5f), boxCollider.size, 0, Vector2.down, 5f, LayerMask.GetMask("Player") );

        if (hitInfo.collider != null)
        {
            isTarget = true;
        }
        else
        {
            isTarget = false;
        }
    }

    public IEnumerator LightningCoroutine()
    {
        float _lastAttackTime = 0;

        while(!GameManager.instance.isGameover)
        {
            if(isTarget)
            {
                if (_lastAttackTime + attackDelay <= Time.time)
                {
                    _lastAttackTime = Time.time;

                    GameObject clone = Instantiate( go_Lightning, new Vector3( transform.position.x - 0.5f, transform.position.y - 1f, 0 ), Quaternion.identity );
                    GameObject clone2 = Instantiate( go_Lightning, new Vector3( transform.position.x + 0.5f, transform.position.y - 1f, 0 ), Quaternion.identity );

                    clone2.GetComponent<SpriteRenderer>().flipX = true;

                    Destroy( clone, 5f );
                    Destroy( clone2, 5f );
                }
            }
            yield return null;
        }
    }
}

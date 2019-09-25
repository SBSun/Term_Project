using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    //몇초마다 미사일을 발사할지
    public float shootTime;
    //마지막으로 미사일을 발사한 시간
    private float lastShootTime = 0;

    //일정 범위 내에 타겠이있으면 타켓의 정보를 담는다.
    public LayerMask layerMask;
    public GameObject go_MissilePrefab;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine( ShootCoroutine() );
    }

    public IEnumerator ShootCoroutine()
    {
        while(!GameManager.instance.isGameover)
        {
            if (Physics2D.OverlapCircle( (Vector2)transform.position, 5f, layerMask ))
            {
                if (lastShootTime + shootTime <= Time.time)
                {
                    lastShootTime = Time.time;
                    //미사일 생성
                    Missile missile = Instantiate( go_MissilePrefab, transform.position, Quaternion.identity ).GetComponent<Missile>();
                    missile.transform.parent = transform;
                    missile.GetComponent<Rigidbody2D>().velocity = Vector2.up * 5;
                    animator.SetTrigger( "Shoot" );
                }
            }
            yield return new WaitForSeconds( 0.1f );
        }
    }
}

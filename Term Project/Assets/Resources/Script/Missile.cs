using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Rigidbody2D rb;
    //현재 스피드, maxSpeed랑 같아질때까지 가속
    public GameObject go_Trail;
    private float currentSpeed = 0;
    //최대 스피드
    public float maxSpeed;
    public bool isFollow = false;
    // Start is called before the first frame update

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    private void Start()
    {
        StartCoroutine( FollowMissileCoroutine() );
    }

    void Update()
    {
        if(isFollow)
        {
            if (currentSpeed < maxSpeed)
                currentSpeed += maxSpeed * Time.deltaTime;

            transform.position += transform.up * currentSpeed * Time.deltaTime;

            Vector3 direction = (GameManager.instance.player.transform.position - transform.position).normalized;

            transform.up = Vector3.Lerp( transform.up, direction, 0.25f );
        }
    }

    public IEnumerator FollowMissileCoroutine()
    {
        yield return new WaitUntil(() => rb.velocity.y < 0 );

        yield return new WaitForSeconds( 0.1f );

        rb.gravityScale = 0;
        go_Trail.SetActive( true );

        isFollow = true;
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerMove>().OnDamage();
            Destroy( gameObject );
        }
        else if (collision.tag == "Monster")
        {
            collision.GetComponent<Monster>().OnDamage();
            Destroy( gameObject );
        }
    }
}

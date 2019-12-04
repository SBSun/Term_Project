using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    // Update is called once per frame

    private void OnCollisionEnter2D( Collision2D collision )
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<PlayerMove>().OnDamage();
            Destroy( gameObject );
        }
        else
        {
            Destroy( gameObject );
        }
    }
}

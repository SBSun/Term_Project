using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float maxPosition;
    public float minPosition;

    public float speed;

    void Update()
    {
        if (transform.localPosition.x < minPosition)
            transform.localPosition = new Vector3( maxPosition, Random.Range( 1.6f, 3.4f ) );

        transform.Translate( Vector3.left * speed * Time.deltaTime );
    }
}

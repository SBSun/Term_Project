using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    public Vector3 movePosition1;
    public Vector3 movePosition2;
    private Vector3 destination;

    public int   speed;

    public int   direction;

    private void Start()
    {
        if (direction == 1)
            destination = movePosition1;
        else if (direction == 2)
            destination = movePosition2;

        StartCoroutine( MoveCoroutine() );
    }

    public IEnumerator MoveCoroutine()
    {
        while (!GameManager.instance.isGameover)
        {
            if (Vector3.Distance( movePosition1, transform.localPosition ) <= 0.1f)
                destination = movePosition2;

            else if (Vector3.Distance( movePosition2, transform.localPosition ) <= 0.1f)
                destination = movePosition1;

            transform.localPosition = Vector3.MoveTowards( transform.localPosition, destination, speed * Time.deltaTime );

            yield return null;
        }
    }
}

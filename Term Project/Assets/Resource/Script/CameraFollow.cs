using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 원래 코드
public class CameraFollow : MonoBehaviour
{
    public GameObject go_Player;

    public bool isMove = true;

    void LateUpdate()
    {
        if (GameManager.instance.isGameover)
            return;

        if (!isMove)
            return;

        transform.position = new Vector3(go_Player.transform.position.x, transform.position.y, transform.position.z );
    }    

    public void StopCamera()
    {
        isMove = false;
    }

    public void MoveCamera()
    {
        isMove = true;
    }
}
*/

public class CameraFollow : MonoBehaviour
{
    public GameObject go_Player;

    public bool isMove = true;
    private double firstTransformY;

    private void Awake()
    {
        firstTransformY = this.transform.position.y;
    }
    void LateUpdate()
    {
        if (GameManager.instance.isGameover)
            return;

        if (!isMove)
            return;

        if (go_Player.transform.position.y >= this.transform.position.y + 1.0f || this.transform.position.y > firstTransformY)
        {
            this.transform.position = new Vector3(go_Player.transform.position.x,
                                                  Mathf.Lerp(this.transform.position.y, go_Player.transform.position.y, Time.deltaTime * 1.5f),
                                                  this.transform.position.z);
        }
        else
            this.transform.position = new Vector3(go_Player.transform.position.x,
                                                  this.transform.position.y,
                                                  this.transform.position.z);
    }

    public void StopCamera()
    {
        isMove = false;
    }

    public void MoveCamera()
    {
        isMove = true;
    }
}
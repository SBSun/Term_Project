using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject go_Player;

    public bool isMove;

    void LateUpdate()
    {
        if (GameManager.instance.isGameover)
            return;

        if (!isMove)
            return;

        transform.localPosition = new Vector3(go_Player.transform.localPosition.x, transform.localPosition.y, transform.localPosition.z );
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

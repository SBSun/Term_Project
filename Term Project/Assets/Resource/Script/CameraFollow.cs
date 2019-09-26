using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject go_Player;

    public float followSpeed;
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

        transform.localPosition = Vector3.Lerp( transform.localPosition, go_Player.transform.localPosition, Time.deltaTime * followSpeed );
        transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y, -1f );
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
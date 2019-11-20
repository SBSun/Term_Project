using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject go_Player;

    public float followSpeed;

    public Vector2 center; //중심점
    public Vector2 size; //크기

    float width;
    float height;

    private void Start()
    {
        height = Camera.main.orthographicSize; //카메라의 세로 절반 길이
        width = height * Screen.width / Screen.height; //카메라의 가로 절반 길이
    }

    void LateUpdate()
    {
        if (GameManager.instance.isGameover)
            return;

        transform.position = Vector3.Lerp( transform.position, GameManager.instance.player.transform.position, Time.deltaTime * followSpeed );

        float mX = size.x * 0.5f - width;
        float clampX = Mathf.Clamp( transform.position.x, -mX + center.x, mX + center.x );

        float mY = size.y * 0.5f - height;
        float clampY = Mathf.Clamp( transform.position.y, -mY + center.y, mY + center.y );

        transform.position = new Vector3( clampX, clampY, -10 );
    }

    public void SetCamera()
    {
        center = GameManager.instance.currentStage.stageCenter;
        size = GameManager.instance.currentStage.stageSize;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube( center, size );
    }
}
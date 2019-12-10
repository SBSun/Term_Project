using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    //게임을 시작할 때 플레이어의 위치
    public Vector3 playerStartPosition;
    public Vector3 cameraStartPosition;

    public Vector2 stageCenter;
    public Vector2 stageSize;

    //해당 스테이지의 요구되는 키 개수
    public int requireKeys;
    //스테이지 번호
    public int stageCode;
}

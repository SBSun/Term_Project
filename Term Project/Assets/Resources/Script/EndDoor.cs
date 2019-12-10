using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    public Sprite sp_openTop;
    public Sprite sp_openMid;
    
    public GameObject go_Top;
    public GameObject go_Mid;

    private bool isDoorOpened;

    private void Start()
    {
        isDoorOpened = false;
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (isDoorOpened == false && GameManager.instance.currentStage.requireKeys <= GameManager.instance.key)
                {
                    // 열리는 소리
                    // 스프라이트 바꾸기
                    go_Top.GetComponent<SpriteRenderer>().sprite = sp_openTop;
                    go_Mid.GetComponent<SpriteRenderer>().sprite = sp_openMid;
                    isDoorOpened = true;
                }
                else if (isDoorOpened == true)
                {
                    // 스테이지 변경
                    GameManager.instance.ChapterSelectStageStart();
                }
                else
                {
                    // 잠긴 소리
                }
            }
        }
    }
}

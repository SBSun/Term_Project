using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerMove    player;
    public SaveLoad      theSaveLoad;
    public Stage         currentStage;
    public Stage[]       stages;
    public bool          isGameover = true;
    public int           score;
    public int           key;   // 스테이지별로 필요한 키의 갯수가 다를 수 있기 때문에 int
    public string        currentUserID;

    private bool         isfirst;

    public bool isPlayingStage
    {
        get
        {
            if (currentStage.stageCode != 0)
                return true;
            else
                return false;
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy( gameObject );

        score = 0;
        key = 0;
        isfirst = true;
        currentUserID = string.Empty;
    }
    
    public void AddScore(int _score)
    {

    }

    public void EndGame()
    {
        isGameover = true;
    }

    public void ChapterSelectStageStart()
    {
        isGameover = false;
        player.curLife = player.maxLife;
        UIManager.instance.StartUI();
        if (isfirst == false) // 처음에 챕터셀렉트 갈땐 currentstage가 부서지면 안되므로
            Destroy(currentStage.gameObject);
        isfirst = false;
        currentStage = stages[0];
        currentStage.gameObject.SetActive( true );
        player.transform.localPosition = currentStage.playerStartPosition;
        Camera.main.GetComponent<CameraFollow>().SetCamera();
        player.gameObject.SetActive( true );
        SoundManager.instance.PlayBGM("BGM_1");
    }

    public void StageStart()
    {
        isGameover = false;
        player.curLife = player.maxLife;
        key = 0;

        Camera.main.GetComponent<CameraFollow>().SetCamera();
        player.transform.localPosition = currentStage.playerStartPosition;
        //stages[0].gameObject.SetActive( false );
        //+추가된부분
        stages[0].gameObject.SetActive(false);
        UIManager.instance.StartUI();
        currentStage = Instantiate(stages[1]);
        GameObject _tgo = GameObject.Find("GameApp");
        currentStage.transform.SetParent(_tgo.transform);
        currentStage.transform.localPosition = new Vector3(0, 0, 0);
        currentStage.gameObject.SetActive( true );
        SoundManager.instance.PlayBGM("Stage_1_BGM");
        player.respawnPosition = currentStage.playerStartPosition;
    }

    public void CheckFlag(GameObject _go) // 플레이어가 flag를 먹으면 flag스크립트에서 실행할 함수
    {
        player.respawnPosition = _go.transform.localPosition;
        Destroy(_go);
    }
}

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
    public GameObject    go_ChapterSelect;
    public bool          isGameover = false;
    public int           score;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy( gameObject );

        score = 0;
    }

    public void AddScore(int _score)
    {

    }

    public void EndGame()
    {
        isGameover = true;
    }

    public void StageStart()
    {
        isGameover = false;
        Camera.main.GetComponent<CameraFollow>().StopCamera();
        Camera.main.transform.localPosition = new Vector3( 0, 0, -1 );
        player.transform.localPosition = currentStage.playerStartPosition;
        player.gameObject.SetActive( true );
        go_ChapterSelect.SetActive( false );
        theSaveLoad.DBRead();
        UIManager.instance.StartUI();
        currentStage.gameObject.SetActive( true );
    }
}

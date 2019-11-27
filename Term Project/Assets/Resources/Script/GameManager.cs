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

    public void ChapterSelectStageStart()
    {
        currentStage = stages[0];
        currentStage.gameObject.SetActive( true );
        player.transform.localPosition = currentStage.playerStartPosition;
        Camera.main.GetComponent<CameraFollow>().SetCamera();
        player.gameObject.SetActive( true );
    }

    public void StageStart()
    {
        isGameover = false;
        player.transform.localPosition = currentStage.playerStartPosition;
        Camera.main.GetComponent<CameraFollow>().SetCamera();
        player.gameObject.SetActive( true );
        stages[0].gameObject.SetActive( false );
        UIManager.instance.StartUI();
        currentStage.gameObject.SetActive( true );
        SoundManager.instance.PlayBGM("Stage_1_BGM");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerMove    player;
    public SaveLoad      theSaveLoad;
    public Stage         currentStage;
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
        currentStage.gameObject.SetActive( true );
        player.transform.localPosition = currentStage.startPosition;
        player.gameObject.SetActive( true );
        theSaveLoad.DBRead();
        UIManager.instance.StartUI();
    }
}

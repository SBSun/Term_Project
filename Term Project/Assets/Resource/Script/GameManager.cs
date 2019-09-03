using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerMove player;
    public GameObject currentStage;
    public bool isGameover = false;
    public int score;

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
}

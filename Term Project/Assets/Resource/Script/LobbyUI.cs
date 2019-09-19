using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    public GameObject go_LobbyUI;

    public void StartButton()
    {
        go_LobbyUI.SetActive( false );
        GameManager.instance.StageStart();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    public GameObject go_LobbyUI;

    public void StartButton()
    {
        go_LobbyUI.SetActive( false );
        GameManager.instance.go_ChapterSelect.SetActive( true );
        GameManager.instance.player.transform.localPosition = new Vector2( -8.13f, -2.46f );
        GameManager.instance.player.gameObject.SetActive( true );
    }
}

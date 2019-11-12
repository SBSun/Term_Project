using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }
    private static UIManager m_instance; //싱글톤이 할당될 변수

    public LoginUI loginUI;
    public LobbyUI lobbyUI;
    public StageUI stageUI;

    public void StartUI()
    {
        stageUI.StartUI();  
    }

    public void FinishUI()
    {
        stageUI.go_StageUI.SetActive( false );
    }
}

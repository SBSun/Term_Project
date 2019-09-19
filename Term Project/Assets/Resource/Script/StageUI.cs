using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    public GameObject go_StageUI;
    public Text playerHp_Text;

    public void StartUI()
    {
        playerHp_Text.text = GameManager.instance.player.curLife.ToString();
        go_StageUI.SetActive( true );
    }

    public void UpdateHpText()
    {
        playerHp_Text.text = GameManager.instance.player.curLife.ToString();
    }
}

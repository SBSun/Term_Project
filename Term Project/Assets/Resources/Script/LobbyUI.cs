using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    public GameObject go_LobbyUI;

    public void StartButton()
    {
        SoundManager.instance.PlaySFX( "Click_1" );

        go_LobbyUI.SetActive( false );
        GameManager.instance.ChapterSelectStageStart();

        /* 데이터베이스 읽어오는 코드
        DataSet ds = SaveLoad.instance.DBReadByAdapter("SELECT * FROM user");
        DataRowCollection rows = ds.Tables[0].Rows;
        foreach (DataRow dr in rows)
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                Debug.Log(dr[i]);
            }
        }
        Debug.Log(rows[0]);
        */
    }
}

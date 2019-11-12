using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;

public class LoginUI : MonoBehaviour
{
    public GameObject loginUIBackground;
    public GameObject LobbyUIBackGruond;
    public GameObject idTextBox;
    public GameObject pwTextBox;


    public void LoginButton()
    {
        string id = idTextBox.GetComponent<InputField>().text;
        string pw = pwTextBox.GetComponent<InputField>().text;
        
        DataSet ds = SaveLoad.instance.DBReadByAdapter("SELECT * FROM user WHERE id=" + "'" + id + "'");
        //DataSet을 사용하려면 using System.Data; 추가

        DataRowCollection rows = ds.Tables[0].Rows; //from으로 가져왔기 때문에 결과테이블은 1개이므로 항상 0번인거같음
        foreach (DataRow dr in rows)
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                Debug.Log(dr[i]);
        }
    }

    public void gotoLobbyButton() // 로그인 완성하면 삭제
    {
        loginUIBackground.SetActive(false); 
        LobbyUIBackGruond.SetActive(true);
    }
}

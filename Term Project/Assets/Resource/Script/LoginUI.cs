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
    public GameObject outputLabel;

    public void LoginButton()
    {
        string id = idTextBox.GetComponent<InputField>().text;
        string pw = pwTextBox.GetComponent<InputField>().text;
        string did = string.Empty;
        string dpw = string.Empty;

        //DataSet을 사용하려면 using System.Data; 추가
        DataSet ds = SaveLoad.instance.DBReadByAdapter("SELECT * FROM user WHERE id=" + "'" + id + "'");
        DataRowCollection rows = ds.Tables[0].Rows; //from으로 가져왔기 때문에 결과테이블은 1개이므로 항상 0번인거같음
                                                    //dataSet에는 여러 테이블이 들어갈 수 있는듯

        //did와 dpw에 가져온 값 저장하기
        foreach (DataRow dr in rows)
        {
            did = dr[0].ToString();
            dpw = dr[1].ToString();
        }

        if (did == string.Empty || dpw == string.Empty)
            UIManager.instance.FadeText(outputLabel, "일치하는 사용자 정보가 없습니다.");
        else
            Debug.Log("did:" + did + "\ndpw:" + dpw);
        
        //비교
        if ((id == did && pw == dpw) && (id!=string.Empty||pw!=string.Empty))
        {
            gotoLobby();
        }
        else
        {
            UIManager.instance.FadeText(outputLabel, "사용자 정보가 일치하지 않습니다.");
        }

    }

    public void gotoLobby()
    {
        loginUIBackground.SetActive(false); 
        LobbyUIBackGruond.SetActive(true);
    }
}

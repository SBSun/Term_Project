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

        //가져온 값 출력
        if (did == string.Empty || dpw == string.Empty)
            Debug.Log("did나 dpw가 비었음");
        else
            Debug.Log("did:" + did + "\ndpw:" + dpw);
        
        //비교
        if (id == did && pw == dpw)
        {
            gotoLobby();
        }
        else
        {
            Debug.Log("로그인 실패 (값이 다름)");
        }

    }

    public void gotoLobby()
    {
        loginUIBackground.SetActive(false); 
        LobbyUIBackGruond.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System;

public class LoginUI : MonoBehaviour
{
    public GameObject loginUIBackground;
    public GameObject LobbyUIBackGruond;
    public GameObject idTextBox;
    public GameObject pwTextBox;
    public GameObject outputLabel;

    private InputField idInputField;
    private InputField pwInputField;

    private void Awake()
    {
        idInputField = idTextBox.GetComponent<InputField>();
        pwInputField = pwTextBox.GetComponent<InputField>();
    }

    private void Update()
    {
        if ( idInputField.isFocused == true)
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                pwInputField.Select();
            }
        }

        if (pwInputField.isFocused == true)
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                idInputField.Select();
            }
        }
    }

    public void LoginButton()
    {
        SoundManager.instance.PlaySFX( "Click_1" );

        string id = idInputField.text;
        string pw = pwInputField.text;
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
            #region masterVolume 초기화
            DataSet ads = SaveLoad.instance.DBReadByAdapter("SELECT volume FROM setting WHERE id=" + "'" + id + "'");
            DataRowCollection arows = ads.Tables[0].Rows;

            string ats = string.Empty; // audio temp string
            foreach (DataRow adr in arows)
            {
                ats = adr[0].ToString();
            }

            if (ats == string.Empty) // volume이 생성이 안되어있으면 
            {
                ats = "0.5";
                SaveLoad.instance.DBInsert("INSERT INTO setting VALUES('" + id + "', 0.5)");
            }

            float atf = (float)Convert.ToDouble(ats);

            if (atf >= 0.0f && atf <= 1.0f) // atd 범위체크
                SoundManager.instance.masterVolume = atf;
            else
            {
                atf = 0.5f;
                SoundManager.instance.masterVolume = atf;
            }
            #endregion
            GameManager.instance.currentUserID = id;
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

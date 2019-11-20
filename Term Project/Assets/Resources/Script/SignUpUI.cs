using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SignUpUI : MonoBehaviour
{

    #region 변수선언부
    public GameObject BackgroundPanel;// 백그라운드 패널을 껐다키며 화면 전환
    public GameObject IDinputField;   // 아이디 입력
    public GameObject PWinputField;   // 비밀번호 입력
    public GameObject REPWinputField; // 비밀번호 재입력
    public GameObject outputLabel;    // 경고메세지를 띄울 라벨

    private string id = string.Empty; // inputfield의 id
    private string pw = string.Empty; // inputfield의 pw
    private string repw = string.Empty;
    private string did = string.Empty;// 데이터베이스에서 가져온 id

    #endregion


    // SignUpUI를 onoff하는 함수
    public void TurnPanel()
    {
        SoundManager.instance.PlaySFX( "Click_1" );

        if (BackgroundPanel.activeSelf)
            BackgroundPanel.SetActive(false);
        else
            BackgroundPanel.SetActive(true);
    }


    // Submit 버튼에 들어갈 함수
    public void SubmitButton()
    {
        SoundManager.instance.PlaySFX( "Click_1" );

        id = IDinputField.GetComponent<InputField>().text;
        pw = PWinputField.GetComponent<InputField>().text;
        repw = REPWinputField.GetComponent<InputField>().text;

        ParsingData(); // id,pw,repw의 공백 제거
        if (isIDPWvalid() == false) // idpw조건에 안맞거나 repw 잘못입력했을경우
        {
            UIManager.instance.FadeText(outputLabel, "idpw조건이 맞지 않거나 repw가 잘못 입력되었습니다.");
            return;
        }

        if (CheckDB() == false) // 아이디가 중복되어 있을경우
        {
            UIManager.instance.FadeText(outputLabel, "이미 같은 id가 존재합니다.");
            return;
        }

        SaveLoad.instance.DBInsert("INSERT INTO user VALUES('"+id+"','"+pw+"')");
        TurnPanel(); // 회원가입 완료.
    }


    // id,pw,repw 파싱
    private void ParsingData()
    {
        id = id.Replace(" ", ""); // 공백제거
        pw = pw.Replace(" ", "");
        repw = repw.Replace(" ", "");
    }


    #region id, pw 검사
    // idpw가 조건에 맞고 pw와 repw가 일치하면 true, 아니면 false
    private bool isIDPWvalid()
    {
        Regex idrx = new Regex("^[A-Za-z0-9]{5,20}$");
        Regex pwrx = new Regex("^[A-Za-z0-9]{5,20}$");

        // id검사
        Match m = idrx.Match(id);
        if (!m.Success) return false;
        // pw검사
        m = pwrx.Match(pw);
        if (!m.Success) return false;
        // repw검사
        if (pw != repw) return false;

        return true;
    }

    // id의 중복을 검사함
    private bool CheckDB()
    {
        DataSet ds = SaveLoad.instance.DBReadByAdapter("SELECT id FROM user WHERE id=" + "'" + id +"'");
        DataRowCollection rows = ds.Tables[0].Rows;
        foreach (DataRow dr in rows)
            did = dr[0].ToString();

        if (did != string.Empty) //did에 값이 있으면
            return false;        //중복 아이디가 있으므로 false

        return true;
    }
    #endregion


    #region 비밀번호를 확인하는 눈동자버튼
    public void eyeButtonDown()
    {
        PWinputField.GetComponent<InputField>().contentType = InputField.ContentType.Standard;
        REPWinputField.GetComponent<InputField>().contentType = InputField.ContentType.Standard;
        PWinputField.GetComponent<InputField>().ForceLabelUpdate();
        REPWinputField.GetComponent<InputField>().ForceLabelUpdate();
    }
    public void eyeButtonUp()
    {
        PWinputField.GetComponent<InputField>().contentType = InputField.ContentType.Password;
        REPWinputField.GetComponent<InputField>().contentType = InputField.ContentType.Password;
        PWinputField.GetComponent<InputField>().ForceLabelUpdate();
        REPWinputField.GetComponent<InputField>().ForceLabelUpdate();
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private bool isPushedButton;

    #region FadeText함수
    IEnumerator IFadeText(GameObject label, string str)
    {
        label.GetComponent<Text>().text = str;
        Color _color = label.GetComponent<Text>().color;
        _color.a = 1.0f;
        label.GetComponent<Text>().color = _color;
        float _time = 0;

        yield return new WaitForSeconds(2.0f);
        while (_time < 1f)
        {
            _time += Time.deltaTime;

            _color.a = Mathf.Lerp(1, 0, _time / 1);
            label.GetComponent<Text>().color = _color;

            yield return null;
        }

        label.GetComponent<Text>().text = "";
        isPushedButton = false;
    }

    public void FadeText(GameObject label, string str)
    {
        if (isPushedButton == false)
        {
            isPushedButton = true;
            instance.StartCoroutine(IFadeText(label, str));
        }
    }
    #endregion
}

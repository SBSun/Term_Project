using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// volSlider가 Awake나 Start하면서 volSliderChanged가 실행되어 자동으로 동기화되는듯
public class PauseUI : MonoBehaviour
{
    public GameObject backgroundPanel;
    public Slider volSlider;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && GameManager.instance.isPlayingStage)
        {
            Time.timeScale = 0.0f;
            volSlider.value = SoundManager.instance.masterVolume;
            backgroundPanel.SetActive(true);
        }
    }

    public void VolSilderChanged() // 슬라이더에 들어갈 코드
    {
        SoundManager.instance.masterVolume = volSlider.value;
    }

    public void ContinueButton()
    {
        SaveLoad.instance.DBUpdate("UPDATE setting SET volume = " + SoundManager.instance.masterVolume + " WHERE id = " + "'" + GameManager.instance.currentUserID + "'" );
        if (Time.timeScale != 1.0f)
        {
            Time.timeScale = 1.0f;
        }
        backgroundPanel.SetActive(false);
    }

    public void GoSelectButton()
    {
        backgroundPanel.SetActive(false);
        GameManager.instance.ChapterSelectStageStart();
    }
}

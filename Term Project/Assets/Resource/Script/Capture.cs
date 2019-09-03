using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 같은 땅을 2개 두고 하나의 땅을 온오프해서 캡쳐 기능을 구현한 스크립트
public class Capture : MonoBehaviour
{
    public GameObject Player;
    public GameObject originGrid;
    public GameObject copiedGrid;

    private bool coolDown; // false가 되면 사용 가능
    private bool isFixed;

    void Start()
    {
        copiedGrid.transform.position = originGrid.transform.position;
        copiedGrid.SetActive(false);
        coolDown = false;
        isFixed = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && coolDown == false)
        {
            coolDown = true;
            copiedGrid.SetActive(true);
            copiedGrid.transform.SetParent(Player.transform);
        }
        else if (Input.GetKeyDown(KeyCode.S) && coolDown == true && isFixed == false)
        {
            isFixed = true;
            copiedGrid.transform.parent = null;
            StartCoroutine("FixGrid");
        }
    }

    IEnumerator FixGrid()
    {
        yield return new WaitForSeconds(3.0f);
        copiedGrid.transform.position = originGrid.transform.position;
        copiedGrid.SetActive(false);
        coolDown = false;
        isFixed = false;
    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && coolDown == false)
        {
            StartCoroutine("DoCapture");
        }
    }

    IEnumerator DoCapture()
    {
        coolDown = true;
        copiedGrid.SetActive(true);
        copiedGrid.transform.SetParent(Player.transform);

        yield return new WaitForSeconds(2.0f);

        copiedGrid.transform.parent = null;

        yield return new WaitForSeconds(3.0f);

        copiedGrid.transform.position = originGrid.transform.position;
        copiedGrid.SetActive(false);
        coolDown = false;
    }
    */


}


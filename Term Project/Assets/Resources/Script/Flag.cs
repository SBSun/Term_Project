using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public Sprite blueFlag2;

    private SpriteRenderer sr;
    private Sprite[] s = new Sprite[2];
    bool anim;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        s[0] = sr.sprite;
        s[1] = blueFlag2;
        anim = true;

        StartCoroutine("FlagAnim");
    }

    IEnumerator FlagAnim()
    {
        while (true)
        {
            if (anim)
            {
                sr.sprite = s[0];
                anim = false;
            }
            else
            {
                sr.sprite = s[1];
                anim = true;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameManager.instance.CheckFlag(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMushroom : MonoBehaviour
{
    private SpriteRenderer sr;
    private int beforeLayer;
    private Sprite s1;

    public Sprite s2;
    public float jumpPower;
    void Start()
    {
        sr = transform.GetComponent<SpriteRenderer>();
        s1 = sr.sprite;
        beforeLayer = this.gameObject.layer;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Monster")
        {
            col.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100 * jumpPower));
            StartCoroutine("Anim");
        }
    }

    IEnumerator Anim()
    {
        this.gameObject.layer = 12;
        sr.sprite = s2;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.layer = beforeLayer;
        sr.sprite = s1;
    }
}

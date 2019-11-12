using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    private SpriteRenderer   spriteRenderer;
    public Sprite            standard_Sprite;
    public Sprite            push_Sprite;

    //버튼이 눌렸을 때 비활성화 될 게임오브젝트들
    public GameObject[] gameObjects;

    //눌린 상태인지
    public bool isPush = false;
    //버튼을 누른뒤 얼마만에 올라올지
    public float returnTime;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator PushCoroutine()
    {
        isPush = true;
        spriteRenderer.sprite = push_Sprite;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive( false );

            yield return null;
        }

        yield return new WaitForSeconds( returnTime );

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive( true );

            yield return null;
        }

        spriteRenderer.sprite = standard_Sprite;
        isPush = false;
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.transform.tag == "Player")
        {
            if (!isPush)
            {
                Debug.Log( "버튼 Push" );
                StartCoroutine( PushCoroutine() );
            }
        }
    }
}

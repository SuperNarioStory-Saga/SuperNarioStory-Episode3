using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEBehaviour : MonoBehaviour
{

    private SpriteRenderer sprend;
    private TextMesh trend;
    private IEnumerator corUp;
    private IEnumerator corDown;

    void Start()
    {
        sprend = transform.GetComponent<SpriteRenderer>();
        trend = transform.GetChild(0).gameObject.GetComponent<TextMesh>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("Player"))
        {
            if(corDown != null)
            {
                StopCoroutine(corDown);
            }
            corUp = Fade(false);
            StartCoroutine(corUp);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (corUp != null)
            {
                StopCoroutine(corUp);
            }
            corDown = Fade(true);
            StartCoroutine(corDown);
        }
    }

    IEnumerator Fade(bool exit)
    {
        if(exit)
        {
            Vector4 col;
            while (sprend.color.a > 0.01)
            {
                col = sprend.color;
                float tex = trend.color.a;
                sprend.color = new Vector4(col.x, col.y, col.z, col.w - 0.07f);
                trend.color = new Vector4(0, 0, 0, tex - 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
            col = sprend.color;
            sprend.color = new Vector4(col.x, col.y, col.z, 0);
            trend.color = Vector4.zero;
        }
        else
        {
            while(sprend.color.a < 0.75)
            {
                Vector4 col = sprend.color;
                float tex = trend.color.a;
                sprend.color = new Vector4(col.x, col.y, col.z, col.w + 0.07f);
                trend.color = new Vector4(0, 0, 0, tex + 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}

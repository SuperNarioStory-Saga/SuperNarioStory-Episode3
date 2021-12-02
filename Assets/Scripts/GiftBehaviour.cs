using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBehaviour : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(BouncingGift());
    }

    IEnumerator BouncingGift()
    {
        float i = 0;
        float j = 0;
        while(true)
        {
            if (transform.position.x > 10.5)
            {
                i = 0;
                transform.position = new Vector3(-10.5f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 0.1f, Mathf.Abs(Mathf.Sin(i) * 1.2f) - 2.53f, transform.position.z);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, j * -15));
                i += 0.1f;
                j += 0.1f;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}

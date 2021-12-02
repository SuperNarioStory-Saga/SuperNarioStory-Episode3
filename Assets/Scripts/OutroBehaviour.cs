using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutroBehaviour : MonoBehaviour
{
    public Sprite giftOK;
    public Sprite giftKO;
    public Sprite end1;
    public Sprite end2;
    public Sprite end3;

    private SpriteRenderer giftRend;
    private SpriteRenderer endRend;
    private Text giftText;
    private Text endText;
    void Start()
    {
        giftRend = transform.GetChild(1).GetComponent<SpriteRenderer>();
        giftText = transform.GetChild(0).GetChild(2).GetComponent<Text>();
        endRend = transform.GetChild(2).GetComponent<SpriteRenderer>();
        endText = transform.GetChild(0).GetChild(3).GetComponent<Text>();

        giftRend.sprite = GlobalScript.giftOwned ? giftOK : giftKO;
        giftText.text = GlobalScript.giftOwned ? "Tu as récupéré le cadeau !" : "Tu n'as pas récupéré le cadeau.";
        Debug.Log(GlobalScript.choice);
        switch (GlobalScript.choice)
        {
            case 1:
                endText.text = "Tu as été gentil avec bawsy";
                endRend.sprite = end1;
                break;
            case 2:
                endText.text = "Tu t'es fait attrapé !";
                endRend.sprite = end2;
                break;
            case 3:
                endText.text = "Tu as marravé bawser";
                endRend.sprite = end3;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
    }
}

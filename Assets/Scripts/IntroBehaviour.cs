using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroBehaviour : MonoBehaviour
{
    
    public AudioClip startSound;

    private bool cinematicStarted;
    private AudioSource audioPlayer;
    private SpriteRenderer sprend;
    private VideoPlayer vidp; 

    void Start()
    {
        cinematicStarted = false;
        audioPlayer = transform.GetComponent<AudioSource>();
        sprend = transform.GetComponent<SpriteRenderer>();
        vidp = transform.GetComponent<VideoPlayer>();
        GlobalScript.giftOwned = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!cinematicStarted)
            {
                StartCoroutine(LaunchGame());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator LaunchGame()
    {
        cinematicStarted = true;
        audioPlayer.Stop();
        audioPlayer.PlayOneShot(startSound);
        while (sprend.color.a < 0.95f)
        {
            sprend.color = new Vector4(0, 0, 0, sprend.color.a + 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
        sprend.color = new Vector4(0, 0, 0, 1);
        yield return new WaitForSeconds(0.4f);
        vidp.Play();
        yield return new WaitForSeconds(10);
        while (vidp.isPlaying)
        {
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("Level");
    }
}

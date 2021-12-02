using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public class Transformation
    {
        public enum Clips { Jump, Hit, Sweep, Land, None };

        public List<Vector3> positions;
        public List<Quaternion> rotations;
        public List<float> speeds;
        public List<bool> rotationOnly;
        public List<Clips> clips;

        public Transformation(Vector3 pos, Vector3 rot)
        {
            positions = new List<Vector3>();
            positions.Add(pos);
            rotations = new List<Quaternion>();
            rotations.Add(Quaternion.Euler(rot.x, rot.y, rot.z));
            speeds = new List<float>();
            speeds.Add(9f);
            rotationOnly = new List<bool>();
            rotationOnly.Add(false);
            clips = new List<Clips>();
            clips.Add(Clips.Jump);
        }

        public void AddClipToLast(Clips clip)
        {
            clips[clips.Count - 1] = clip;
        }
        public void Add(Vector3 pos, Vector3 rot)
        {
            positions.Add(pos);
            rotations.Add(Quaternion.Euler(rot.x, rot.y, rot.z));
            speeds.Add(9f);
            rotationOnly.Add(false);
            clips.Add(Clips.None);
        }

        public void Add(Vector3 pos, Vector3 rot, float speed)
        {
            positions.Add(pos);
            rotations.Add(Quaternion.Euler(rot.x, rot.y, rot.z));
            speeds.Add(speed);
            rotationOnly.Add(false);
            clips.Add(Clips.None);
        }

        public void Add(Vector3 pos, Vector3 rot, bool roton)
        {
            positions.Add(pos);
            rotations.Add(Quaternion.Euler(rot.x, rot.y, rot.z));
            speeds.Add(9f);
            rotationOnly.Add(roton);
            clips.Add(Clips.None);
        }

        public void Add(Vector3 pos, Vector3 rot, float speed, bool roton)
        {
            positions.Add(pos);
            rotations.Add(Quaternion.Euler(rot.x, rot.y, rot.z));
            speeds.Add(speed);
            rotationOnly.Add(roton);
            clips.Add(Clips.None);
        }
    }

    public float speed;
    public AudioClip clipDeath;
    public AudioClip clipHit;
    public AudioClip clipLand;
    public AudioClip clipSweep;
    public AudioClip clipJump;
    public AudioClip clipGift;
    public AudioClip clipTeleport;
    public GameObject portalO;
    public GameObject portalB;

    private List<Transformation> transformations;
    private bool isAnim;

    private Rigidbody2D rb2d;
    private BoxCollider2D bcollid;
    private SpriteRenderer sprend;
    private Animator animControl;
    private AudioSource audiosrc;
    private AudioSource audioThemesrc;

    void Start()
    {   
        rb2d = transform.GetComponent<Rigidbody2D>();
        bcollid = transform.GetComponent<BoxCollider2D>();
        sprend = transform.GetComponent<SpriteRenderer>();
        animControl = transform.GetComponent<Animator>();
        audiosrc = transform.GetComponents<AudioSource>()[0];
        audioThemesrc = transform.GetComponents<AudioSource>()[1];

        transformations = new List<Transformation>();
        isAnim = false;
        /*QTEs*/
        //1
        Transformation qte1 = new Transformation(new Vector3(-33.49f, -3.12f, 0), Vector3.zero);
        qte1.Add(new Vector3(-33.49f, -0.43f, 0), Vector3.zero);
        qte1.Add(new Vector3(-32.48f, 2.26f, 0), Vector3.zero);
        qte1.Add(new Vector3(-31.61f, 0.47f, 0), Vector3.zero);
        transformations.Add(qte1);
        //2
        Transformation qte2 = new Transformation(new Vector3(-31.61f, 0.47f, 0), Vector3.zero);
        qte2.Add(new Vector3(-31.66f, 3.05f, 0), Vector3.zero, 12);
        qte2.Add(new Vector3(-31.66f, 3.05f, 0), new Vector3(0, 0, -90), true);
        qte2.AddClipToLast(Transformation.Clips.Sweep);
        qte2.Add(new Vector3(-26.32f, 3.3f, 0), Vector3.zero, 12);
        qte2.Add(new Vector3(-26.32f, 3.3f, 0), Vector3.zero, true);
        qte2.AddClipToLast(Transformation.Clips.Sweep);
        transformations.Add(qte2);
        //3
        Transformation qte3 = new Transformation(new Vector3(-26.3f, 0.5f, 0), Vector3.zero);
        qte3.Add(new Vector3(-24.95f, 2.34f, 0), Vector3.zero);
        qte3.Add(new Vector3(-22.34f, 2.63f, 0), Vector3.zero);
        transformations.Add(qte3);
        //4
        Transformation qte4 = new Transformation(new Vector3(-22.98f, -3.13f, 0), Vector3.zero);
        qte4.Add(new Vector3(-24.02f, -1.34f, 0), Vector3.zero);
        qte4.Add(new Vector3(-24.02f, -1.34f, 0), new Vector3(0, 0, -70), 40, true);
        qte4.AddClipToLast(Transformation.Clips.Land);
        qte4.Add(new Vector3(-21.04f, -0.39f, 0), Vector3.zero);
        qte4.Add(new Vector3(-21.04f, -0.39f, 0), new Vector3(0, 0, -126), 40, true);
        qte4.AddClipToLast(Transformation.Clips.Sweep);
        qte4.Add(new Vector3(-17.47f, -2.51f, 0), Vector3.zero);
        qte4.Add(new Vector3(-17.47f, -2.51f, 0), Vector3.zero, 40, true);
        qte4.AddClipToLast(Transformation.Clips.Land);
        transformations.Add(qte4);
        //5
        Transformation qte5 = new Transformation(new Vector3(-19.66f, -3.17f, 0), Vector3.zero);
        qte5.Add(new Vector3(-17.45f, -0.84f, 0), Vector3.zero);
        qte5.Add(new Vector3(-16.15f, -2.13f, 0), new Vector3(0, 0, -70));
        qte5.Add(new Vector3(-9.86f, -3.18f, 0), new Vector3(0, 0, -70), 14);
        qte5.Add(new Vector3(-9.86f, -3.18f, 0), Vector3.zero, 50, true);
        transformations.Add(qte5);
        //6
        Transformation qte6 = new Transformation(new Vector3(-17.53f, -2.3f, 0), Vector3.zero);
        qte6.Add(new Vector3(-17.39f, 3.06f, 0), Vector3.zero);
        qte6.Add(new Vector3(-19.46f, 2.58f, 0), Vector3.zero);
        transformations.Add(qte6);
        //7
        Transformation qte7 = new Transformation(new Vector3(-19.38f, 2.55f, 0), Vector3.zero);
        qte7.Add(new Vector3(-11.13f, 3.79f, 0), Vector3.zero);
        qte7.Add(new Vector3(-13.24f, 2.86f, 0), new Vector3(0, 0, -70), 6);
        qte7.AddClipToLast(Transformation.Clips.Sweep);
        qte7.Add(new Vector3(-13.24f, 2.86f, 0), Vector3.zero, 50, true);
        qte7.Add(new Vector3(-13.28f, 0.73f, 0), Vector3.zero, 50, true);
        transformations.Add(qte7);
        //8
        Transformation qte8 = new Transformation(new Vector3(-12.99f, 0.73f, 0), Vector3.zero);
        qte8.Add(new Vector3(-10.37f, 3, 0), new Vector3(0, 0, -50), 8);
        qte8.Add(new Vector3(-6.7f, -0.56f, 0), new Vector3(0, 0, -50), 8);
        qte8.Add(new Vector3(-6.7f, -0.56f, 0), Vector3.zero, 30, true);
        qte8.AddClipToLast(Transformation.Clips.Land);
        transformations.Add(qte8);
        //9
        Transformation qte9 = new Transformation(new Vector3(-9.64f, -3.18f, 0), Vector3.zero);
        qte9.Add(new Vector3(-9.21f, -1.7f, 0), Vector3.zero, 7);
        qte9.Add(new Vector3(-8.75f, -2.28f, 0), Vector3.zero, 7);
        qte9.AddClipToLast(Transformation.Clips.Jump);
        qte9.Add(new Vector3(-8.37f, -0.71f, 0), Vector3.zero, 7);
        qte9.Add(new Vector3(-7.82f, -1.44f, 0), Vector3.zero, 7);
        qte9.AddClipToLast(Transformation.Clips.Jump);
        qte9.Add(new Vector3(-7.35f, 0.3f, 0), Vector3.zero, 7);
        qte9.Add(new Vector3(-6.8f, -0.56f, 0), Vector3.zero, 7);
        qte9.AddClipToLast(Transformation.Clips.Jump);
        transformations.Add(qte9);
        //10
        Transformation qte10 = new Transformation(new Vector3(-3.05f, -0.55f, 0), Vector3.zero);
        qte10.Add(new Vector3(-0.17f, 2.58f, 0), Vector3.zero);
        qte10.Add(new Vector3(2.74f, 1.52f, 0), Vector3.zero);
        qte10.AddClipToLast(Transformation.Clips.Jump);
        qte10.Add(new Vector3(5.23f, 3.2f, 0), Vector3.zero);
        qte10.AddClipToLast(Transformation.Clips.Jump);
        qte10.Add(new Vector3(8.17f, 2.5f, 0), Vector3.zero);
        qte10.AddClipToLast(Transformation.Clips.Land);
        transformations.Add(qte10);
        //11
        Transformation qte11 = new Transformation(new Vector3(7.3f, 0.33f, 0), Vector3.zero);
        qte11.Add(new Vector3(7.3f, 0.33f, 0), new Vector3(0, 0, 90), 15, true);
        qte11.AddClipToLast(Transformation.Clips.Sweep);
        qte11.Add(new Vector3(5.2f, -0.93f, 0), Vector3.zero);
        qte11.Add(new Vector3(0.52f, -1.2f, 0), Vector3.zero);
        qte11.Add(new Vector3(0.52f, -1.2f, 0), Vector3.zero, 15, true);
        qte11.AddClipToLast(Transformation.Clips.Land);
        qte11.Add(new Vector3(1.15f, -0.5f, 0), Vector3.zero);
        transformations.Add(qte11);
        //12
        Transformation qte12 = new Transformation(new Vector3(6.389f, -0.568f, 0), Vector3.zero);
        qte12.Add(new Vector3(6.34f, 4.1f, 0), Vector3.zero, 15);
        qte12.Add(new Vector3(6.34f, 4.1f, 0), new Vector3(0, 0, -141.6f), true);
        qte12.AddClipToLast(Transformation.Clips.Sweep);
        qte12.Add(new Vector3(8.22f, 2.5f, 0), Vector3.zero);
        qte12.Add(new Vector3(8.22f, 2.5f, 0), Vector3.zero, 40, true);
        qte12.AddClipToLast(Transformation.Clips.Land);
        qte12.Add(new Vector3(8.24f, 2.5f, 0), Vector3.zero);
        transformations.Add(qte12);
        //13
        Transformation qte13 = new Transformation(new Vector3(10.82f, 2.5f, 0), Vector3.zero);
        qte13.Add(new Vector3(11.98f, 2.49f, 0), Vector3.zero);
        qte13.Add(new Vector3(13.25f, -0.53f, 0), Vector3.zero);
        qte13.AddClipToLast(Transformation.Clips.Land);
        transformations.Add(qte13);
        //14
        Transformation qte14 = new Transformation(new Vector3(15.06f, -0.56f, 0), Vector3.zero);
        qte14.Add(new Vector3(18.26f, 1.69f, 0), Vector3.zero);
        qte14.Add(new Vector3(24.31f, 1.62f, 0), Vector3.zero);
        qte14.Add(new Vector3(24.31f, -3.15f, 0), Vector3.zero, 16);
        qte14.AddClipToLast(Transformation.Clips.Land);
        transformations.Add(qte14);
        //15
        Transformation qte15 = new Transformation(new Vector3(18.93f, -3.2f, 0), Vector3.zero);
        qte15.Add(new Vector3(20.02f, -0.56f, 0), Vector3.zero);
        qte15.Add(new Vector3(22.36f, 1.03f, 0), Vector3.zero);
        transformations.Add(qte15);
        //16
        Transformation qte16 = new Transformation(new Vector3(22.38f, -0.65f, 0), Vector3.zero);
        qte16.Add(new Vector3(24.15f, 1.35f, 0), Vector3.zero);
        qte16.Add(new Vector3(26.65f, 2.58f, 0), Vector3.zero);
        transformations.Add(qte16);
        //17
        Transformation qte17 = new Transformation(new Vector3(26.68f, -0.05f, 0), Vector3.zero);
        qte17.Add(new Vector3(27.26f, 2.23f, 0), Vector3.zero, 8);
        qte17.Add(new Vector3(28.08f, 1.38f, 0), Vector3.zero, 8);
        qte17.Add(new Vector3(28.76f, 2.6f, 0), Vector3.zero, 8);
        qte17.Add(new Vector3(29.34f, 1.61f, 0), Vector3.zero, 8);
        qte17.AddClipToLast(Transformation.Clips.Land);
        transformations.Add(qte17);
        //18
        Transformation qte18 = new Transformation(new Vector3(24.71f, -3.16f, 0), Vector3.zero);
        qte18.Add(new Vector3(24.71f, 2.79f, 0), Vector3.zero, 16);
        qte18.Add(new Vector3(30.86f, 3.79f, 0), new Vector3(0, 0, 50));
        qte18.AddClipToLast(Transformation.Clips.Sweep);
        qte18.Add(new Vector3(30.86f, 3.79f, 0), Vector3.zero, 40, true);
        qte18.AddClipToLast(Transformation.Clips.Sweep);
        transformations.Add(qte18);
        //19
        Transformation qte19 = new Transformation(new Vector3(29.37f, 1.61f, 0), Vector3.zero);
        qte19.Add(new Vector3(30.79f, 1.57f, 0), Vector3.zero, 5);
        qte19.Add(new Vector3(30.87f, -3.14f, 0), new Vector3(0, 0, -50));
        qte19.AddClipToLast(Transformation.Clips.Sweep);
        qte19.Add(new Vector3(36.13f, -3.14f, 0), new Vector3(0, 0, -50));
        qte19.AddClipToLast(Transformation.Clips.Sweep);
        qte19.Add(new Vector3(26.56f, -3.14f, 0), new Vector3(0, 0, -50));
        qte19.AddClipToLast(Transformation.Clips.Sweep);
        qte19.Add(new Vector3(26.56f, -3.14f, 0), Vector3.zero, 40, true);
        transformations.Add(qte19);
        //20
        Transformation qte20 = new Transformation(new Vector3(30.87f, -0.11f, 0), Vector3.zero);
        qte20.Add(new Vector3(31.02f, 3.07f, 0), Vector3.zero);
        qte20.Add(new Vector3(31.95f, 2.53f, 0), Vector3.zero, 10);
        qte20.Add(new Vector3(35.52f, 3.62f, 0), Vector3.zero, 11);
        qte20.AddClipToLast(Transformation.Clips.Jump);
        qte20.Add(new Vector3(35.52f, 3.62f, 0), new Vector3(0, 0, -145), 13, true);
        qte20.Add(new Vector3(38.83f, -1.73f, 0), Vector3.zero, 16);
        transformations.Add(qte20);
        //21
        Transformation qte21 = new Transformation(new Vector3(37.67f, -3.19f, 0), Vector3.zero);
        qte21.Add(new Vector3(38.28f, -0.46f, 0), Vector3.zero);
        qte21.Add(new Vector3(39.1f, -1.63f, 0), Vector3.zero);
        transformations.Add(qte21);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        //Controle par le joueur
        if (!isAnim)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(moveHorizontal, 0);
            rb2d.velocity = movement * speed;
            animControl.SetBool("isWalking", movement.x != 0);
            if (movement.x != 0)
            {
                sprend.flipX = movement.x < 0;
            }
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isAnim && collision.CompareTag("QTE"))
        {
            if (Input.GetKeyDown(collision.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text.ToLower()))
            {
                Regex rx = new Regex(@"^\w*\s\((\d+)\)");
                Match m = rx.Match(collision.gameObject.name);
                int qteID = int.Parse(m.Groups[1].Captures[0].Value);
                StartCoroutine(DoAnim(qteID));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gift"))
        {
            StartCoroutine(Gift(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("Portal"))
        {
            if (collision.gameObject.name.Equals("PortalBlue"))
            {
                transform.position = new Vector3(9.55f, -0.52f, 0);
            }
            else
            {
                transform.position = new Vector3(0.55f, -0.63f, 0);
            }
            audiosrc.PlayOneShot(clipTeleport);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isAnim)
            {
                StartCoroutine(Death());
            }
            else
            {
                Destroy(collision.gameObject);
                audiosrc.PlayOneShot(clipHit);
            }
        }
    }

    IEnumerator Gift(GameObject gift)
    {
        GlobalScript.giftOwned = true;
        yield return new WaitForSeconds(1f);
        audiosrc.PlayOneShot(clipGift);
        Destroy(gift);
    }

    IEnumerator Death()
    {
        isAnim = true;
        sprend.enabled = false;
        bcollid.enabled = false;
        rb2d.gravityScale = 0;
        rb2d.velocity = Vector2.zero;
        while(audioThemesrc.volume > 0.3)
        {
            audioThemesrc.volume -= 0.25f;
            yield return new WaitForSeconds(0.02f);
        }
        audiosrc.PlayOneShot(clipDeath);
        while (audioThemesrc.volume < 1)
        {
            audioThemesrc.volume += 0.05f;
            yield return new WaitForSeconds(0.04f);
        }
        audioThemesrc.volume = 1;
        sprend.enabled = true;
        bcollid.enabled = true;
        rb2d.gravityScale = 32;
        transform.position = new Vector3(-39, -1.59f, 0);
        isAnim = false;
    }

    IEnumerator DoAnim(int idAnim)
    {
        isAnim = true;
        rb2d.velocity = Vector2.zero;
        rb2d.gravityScale = 0;
        animControl.SetBool("isWalking", false);
        int animState = 0;
        Debug.Log("Launch anim " + idAnim + ", contains " + transformations[idAnim].positions.Count + " steps");
        while (animState < transformations[idAnim].positions.Count)
        {
            Debug.Log("Step " + animState);
            //Sound effect
            switch(transformations[idAnim].clips[animState])
            {
                case Transformation.Clips.Jump:
                    audiosrc.PlayOneShot(clipJump);
                    break;
                case Transformation.Clips.Hit:
                    audiosrc.PlayOneShot(clipHit);
                    break;
                case Transformation.Clips.Land:
                    audiosrc.PlayOneShot(clipLand);
                    break;
                case Transformation.Clips.Sweep:
                    audiosrc.PlayOneShot(clipSweep);
                    break;
            }
            //Physics transformations
            if (transformations[idAnim].rotationOnly[animState])
            {
                do
                {
                    //Debug.Log("Anim iter rot");
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, transformations[idAnim].rotations[animState], transformations[idAnim].speeds[animState] * 30 * Time.deltaTime);
                    yield return new WaitForSeconds(0.01f);
                } while (Quaternion.Angle(transformations[idAnim].rotations[animState], transform.rotation) > 0.1f);
            }
            else
            {
                do
                {
                    //Debug.Log("Anim iter pos");
                    transform.position = Vector3.MoveTowards(transform.position, transformations[idAnim].positions[animState], transformations[idAnim].speeds[animState] * Time.deltaTime);
                    if(transformations[idAnim].rotations[animState].z != 0)
                    {
                        transform.Rotate(new Vector3(0, 0, transform.rotation.eulerAngles.z + transformations[idAnim].rotations[animState].z));
                    }    
                    yield return new WaitForSeconds(0.01f);
                } while (Vector3.Distance(transformations[idAnim].positions[animState], transform.position) > 0.1f);
            }
            animState++;
        }
        isAnim = false;
        rb2d.gravityScale = 32;
        if(idAnim == 10)
        {
            portalO.SetActive(true);
            portalB.SetActive(true);
        }
    }
    
}

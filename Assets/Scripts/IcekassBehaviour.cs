using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcekassBehaviour : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb2d;
    private Animator animControl;
    private SpriteRenderer sprend;
    private Vector3 startVector;
    private bool moveToRight;
    private bool isAttacking;
    private bool hasAttacked;

    void Start()
    {
        startVector = transform.position;
        moveToRight = false;
        isAttacking = false;
        hasAttacked = false;

        rb2d = transform.GetComponent<Rigidbody2D>();
        sprend = transform.GetComponent<SpriteRenderer>();
        animControl = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x < startVector.x - 1.25f && !hasAttacked) //Trigger attack
        {
            rb2d.velocity = Vector2.zero;
            isAttacking = true;
            hasAttacked = true;
            animControl.SetBool("isWalking", false);
            animControl.SetBool("isAttacking", true);
            StartCoroutine(Attack());
        }
        else if(!isAttacking) //Prevent icekass from walking casually during attack time
        {
            animControl.SetBool("isWalking", true);
            if (transform.position.x < startVector.x - 2.5f || moveToRight) //Left border
            {
                Vector2 movement = new Vector2(1, 0);
                rb2d.velocity = movement * speed;
                moveToRight = true;
                sprend.flipX = true;
            }
            else
            {
                Vector2 movement = new Vector2(-1, 0);
                rb2d.velocity = movement * speed;
                sprend.flipX = false;
            }
            if (transform.position.x > startVector.x) //Right border
            {
                moveToRight = false;
                hasAttacked = false;
            }
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        animControl.SetBool("isAttacking", false);
        yield return new WaitForSeconds(2f);
        animControl.SetBool("isWalking", true);
        isAttacking = false;
    }
}

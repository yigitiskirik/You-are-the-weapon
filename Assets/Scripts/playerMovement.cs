using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public static playerMovement instance;

    public float movementSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    private Camera theCam;

    //public Animator Anim;

    public SpriteRenderer SR;
    public Sprite[] sprites; 

    private float activeMoveSpeed;
    public float dashSpeed = 8f, dashLenght = .5f, dashCooldown = 1f, dashInvinc;
    [HideInInspector]
    public float dashCounter;
    private float dashCoolCounter;

    [HideInInspector]
    public bool canMove = true;
    public Transform TR;

    public float friction = 0.1f;

    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        theCam = Camera.main;

        activeMoveSpeed = movementSpeed;

        TR = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {


            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            moveInput.Normalize();

            //transform.position += new Vector3(moveInput.x * Time.deltaTime * movementSpeed, moveInput.y * Time.deltaTime * movementSpeed, 0f);

            //theRB.linearVelocity = moveInput * activeMoveSpeed;
            float theX = Mathf.Lerp(theRB.linearVelocity.x, moveInput.x * activeMoveSpeed, friction * Time.deltaTime);
            float theY = Mathf.Lerp(theRB.linearVelocity.y, moveInput.y * activeMoveSpeed, friction * Time.deltaTime);
            theRB.linearVelocity = new Vector2(theX, theY);

            if (moveInput.x > 0)
            {
                SR.sprite = sprites[1];
                TR.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (moveInput.x < 0)
            {
                
                 SR.sprite = sprites[1];
                 TR.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            else if (moveInput.y < 0)
            {
                SR.sprite = sprites[0];
            }
            else if (moveInput.y > 0)
            {
                SR.sprite = sprites[2];
            }


            if (Input.GetKeyDown(KeyCode.Space) && dashCoolCounter <= 0 && dashCounter <= 0)
            {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLenght;

                    //Anim.SetTrigger("Dash");
            }
            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    activeMoveSpeed = movementSpeed;
                    dashCoolCounter = dashCooldown;
                }
            }
            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }

            //animation
            if (moveInput != Vector2.zero)
            {
                //Anim.SetBool("IsMoving", true);
            }
            else
            {
                //Anim.SetBool("IsMoving", false);
            }
        }
        /*else
        {
            if (theRB.linearVelocity.magnitude == 0) return;
            float x = theRB.linearVelocity.x - Time.deltaTime * friction;
            float y = theRB.linearVelocity.y - Time.deltaTime * friction;
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            theRB.linearVelocity = new Vector2(x, y);
            
            //Anim.SetBool("IsMoving", false);
        }*/
    }
}
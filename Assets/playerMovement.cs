using System.Collections;
using System.Collections.Generic;
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


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        theCam = Camera.main;

        activeMoveSpeed = movementSpeed;
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

            theRB.linearVelocity = moveInput * activeMoveSpeed;

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

            /*if (mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
                gunArm.localScale = Vector3.one;
            }
            */

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLenght;

                    //Anim.SetTrigger("Dash");
                }


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
        else
        {
            theRB.linearVelocity = Vector2.zero;
            //Anim.SetBool("IsMoving", false);
        }   
    }
}
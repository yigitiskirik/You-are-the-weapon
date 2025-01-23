using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;

    public float rangeToChasePlayer, distanceToPlayer;
    private Vector3 moveDirection;

    //public Animator Anim;

    public int health = 150;

    //public GameObject deathEffect;
    //public GameObject EnemyHitEffect;

    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;

    public float shootRange;

    public SpriteRenderer theBody;

    public void Awake()
    {
        //this.theBody = this.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (theBody.isVisible)
        {
            if (theBody.isVisible && playerMovement.instance.gameObject.activeInHierarchy)
            {
                if (Vector3.Distance(transform.position, playerMovement.instance.transform.position) < rangeToChasePlayer && Vector3.Distance(transform.position, playerMovement.instance.transform.position) > distanceToPlayer)
                {
                    moveDirection = playerMovement.instance.transform.position - transform.position;
                }
                else
                {
                    moveDirection = Vector3.zero;
                }


                moveDirection.Normalize();

                theRB.linearVelocity = moveDirection * moveSpeed;



                if (shouldShoot && Vector3.Distance(transform.position, playerMovement.instance.transform.position) < shootRange)
                {
                    fireCounter -= Time.deltaTime;

                    if (fireCounter <= 0)
                    {
                        fireCounter = fireRate;
                        Instantiate(bullet, firePoint.transform.position, transform.rotation);
                    }
                }

            }
            else
            {
                theRB.linearVelocity = Vector2.zero;
            }

        }

        if (moveDirection != Vector3.zero)
        {
            //Anim.SetBool("IsMoving", true);
        }
        else
        {
            //Anim.SetBool("IsMoving", false);
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        theBody.color = Color.red;
        theBody.color = Color.white;
        //Instantiate(EnemyHitEffect, transform.position, transform.rotation);

        if (health <= 0)
        {
            Destroy(gameObject);

            //Instantiate(deathEffect, transform.position, transform.rotation);

        }
    }

}

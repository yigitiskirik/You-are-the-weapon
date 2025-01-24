using System;
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
    //public GameObject testBullet;
    public EnemyBullet bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;

    public float shootRange;

    public enum ShootPatternType
    {
        ToPlayer,
        Plus,
        Shotgun,
        OmniDirectional,
        XMark,
    }

    public EnemyBullet.BulletType bulletType;
    public ShootPatternType shootingPattern;

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
                        bullet.type = bulletType;
                        switch (bulletType)
                        {
                            case EnemyBullet.BulletType.bouncy:
                                bullet.speed = 5;
                                bullet.bounceLife = 4;
                                bullet.homingDistance = 0;
                                break;
                            case EnemyBullet.BulletType.normal:
                                bullet.speed = 10;
                                bullet.bounceLife = 0;
                                bullet.homingDistance = 0;
                                break;
                            case EnemyBullet.BulletType.homing:
                                bullet.speed = 5;
                                bullet.bounceLife = 0;
                                bullet.homingDistance = 2;
                                break;
                        }
                        switch (shootingPattern)
                        {
                            case ShootPatternType.ToPlayer:
                                bullet.directional = false;
                                bullet.shootAngle = 0;
                                Instantiate(bullet, firePoint.transform.position, transform.rotation);
                                break;
                            case ShootPatternType.Plus:
                                bullet.directional = true;
                                for (int i = 0; i < 4; i++)
                                {
                                    bullet.shootAngle = i * 90;
                                    Instantiate(bullet, firePoint.transform.position, transform.rotation);
                                }
                                break;
                            case ShootPatternType.XMark:
                                bullet.directional = true;
                                for (int i = 0; i < 4; i++)
                                {
                                    bullet.shootAngle = i * 90 + 45;
                                    Instantiate(bullet, firePoint.transform.position, transform.rotation);
                                }
                                break;
                            case ShootPatternType.OmniDirectional:
                                bullet.directional = true;
                                for (int i = 0; i < 8; i++)
                                {
                                    bullet.shootAngle = i * 45;
                                    Instantiate(bullet, firePoint.transform.position, transform.rotation);
                                }
                                break;
                            case ShootPatternType.Shotgun:
                                bullet.directional = false;
                                bullet.shootAngle = -15;
                                Instantiate(bullet, firePoint.transform.position, transform.rotation);
                                bullet.shootAngle = 0;
                                Instantiate(bullet, firePoint.transform.position, transform.rotation);
                                bullet.shootAngle = 15;
                                Instantiate(bullet, firePoint.transform.position, transform.rotation);
                                break;
                        }
                        //Instantiate(testBullet, firePoint.transform.position, transform.rotation);
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

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Vector3 direction;

    public enum BulletType {
        normal,
        bouncy,
        homing
    }

    public BulletType type;

    public int bounceLife;

    // Start is called before the first frame update
    void Start()
    {
        direction = playerMovement.instance.transform.position - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += direction * speed * Time.deltaTime;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("hit");
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        else if (type == BulletType.normal)
        {
            Destroy(gameObject);
        }
        else if (type == BulletType.bouncy)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                var firstContact = other.contacts[0];
                direction = Vector2.Reflect(direction.normalized, firstContact.normal);
                direction.Normalize();
                bounceLife--;

                if (bounceLife == 0) Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{

    Rigidbody2D physics;
    [SerializeField] float speed;
    public bool isRight;
    [SerializeField] int forceDamage;

    void Start()
    {
        this.physics = GetComponent<Rigidbody2D>();
        this.speed = 10;
        this.forceDamage = 100;
        Destroy(this.gameObject, 2f);
    }

    
    void FixedUpdate()
    {
        if(isRight)
        {
            physics.velocity = Vector2.right * speed;
        } else
        {
            physics.velocity = Vector2.left * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<PatrolGuyController>().Damage(forceDamage);
            Destroy(this.gameObject);
        }
    }

}

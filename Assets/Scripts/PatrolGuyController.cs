using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGuyController : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float walkTime;
    [SerializeField] float Timer;
    [SerializeField] int health;
    [SerializeField] int forceDamage;

    Animator animator;
    Rigidbody2D physics;
    public bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        this.physics = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.speed = 1;
        this.walkTime = 3;
        this.forceDamage = 100;
        this.isRight = false;
        this.health = 300;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.Timer += Time.deltaTime;

        if(this.Timer >= walkTime)
        {
            this.Timer = 0;
            this.isRight = !this.isRight;
        }

        if(isRight)
        {
            transform.eulerAngles = new Vector2(0, 180);
            physics.velocity = Vector2.right * speed;
        } else
        {
            transform.eulerAngles = new Vector2(0, 0);
            physics.velocity = Vector2.left * speed;
        }

    }

    public void Damage(int dmg)
    {

        this.health -= dmg;
        Debug.Log(this.health);
        animator.SetTrigger("hit");
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Damage(forceDamage);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    enum animationType : int
    {
        Idle = 0,
        Run = 1,
        Jump = 2,
        Bow = 3
    }

    public float speed;
    public float jumpforce;
    int limitJump;
    int countJump;
    const int layerFloor = 6;

    Rigidbody2D physical;
    Animator animator;
    float movement;
    bool imortal;
    SpriteRenderer sprite;

    [SerializeField] GameObject arrowTrigger;
    [SerializeField] GameObject arrow;
    [SerializeField] int health;

    private void Awake()
    {
        this.physical = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
        this.speed = 5;
        this.health = 3;
        this.imortal = false;
        this.jumpforce = 5;
        this.limitJump = 2;
        this.countJump = 0;
    }

    void Update()
    {
        MoveToRun();
        MoveToJump();
        AttackBow();

        if(Input.GetKeyDown(KeyCode.Z))
        {
            physical.AddForce(Vector2.left * 50, ForceMode2D.Impulse);
            Debug.Log("right force");
        }

        UIController.instance.UpdateLives(health);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == layerFloor)
        {
            this.countJump = 0;
            this.animateToIdle();
        }
    }

    /*
        Move list
    */
    void MoveToRun()
    {
        movement = Input.GetAxis("Horizontal");
        if(movement > 0)
        {
            this.transform.eulerAngles = new Vector3(0,0,0); //-> Right
            this.animateToRun();
        } else if (movement < 0) { 
            this.transform.eulerAngles = new Vector3(0, 180, 0); //-> Left
            this.animateToRun();
        } else
        {
            this.animateToIdle();
        }
        physical.velocity = new Vector2(movement * this.speed, physical.velocity.y);
    }

    void MoveToJump()
    {

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) && (this.countJump < this.limitJump))
        {
            physical.AddForce(new Vector2(0,jumpforce), ForceMode2D.Impulse);
            this.countJump += 1;
            this.animateToJump();
        }
        
    }

    void gameOver()
    {
        Time.timeScale = 0;
    }

    /*
        Attack List
    */
    public void Damage(int dmg)
    {

        if (imortal) { return; }
        this.health -= (dmg / 100);
        imortal = true;
        sprite = this.GetComponent<SpriteRenderer>();

        //Vector2 direction = (transform.rotation.y == 180) ? Vector2.right : Vector2.left;
        //physical.AddForce(direction * 50, ForceMode2D.Impulse);

        float direction = (transform.rotation.y == 180) ? 0.5f : -0.5f;
        transform.position += new Vector3(direction, 0, 0);

        if (this.health == 0)
        {
            this.gameOver();
        }
        else
        {
            StartCoroutine(DamageEffect());
        }
    }

    public void incrementLife(int life)
    {
        this.health += life;
    }

    public IEnumerator DamageEffect()
    {
        sprite.color = new Color(1f, 0, 0, 1f);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1f, 1f, 1f, 1f);

        for (int i = 0; i < 7; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.15f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.15f);
        }

        imortal = false;

    }

    void AttackBow()
    {
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Fire");
            animator.SetInteger("transition", (int) animationType.Bow);
            GameObject objArrow = Instantiate(arrow, arrowTrigger.transform.position, arrowTrigger.transform.rotation);

            objArrow.GetComponent<ArrowController>().isRight = (transform.rotation.y == 0);

            yield return new WaitForSeconds(0.5f);
            animator.SetInteger("transition", (int) animationType.Idle);
        }
    }

    /*
        Animations list
    */
    void animateToIdle()
    {
        if (this.countJump == 0 && animator.GetInteger("transition") != (int)animationType.Bow)
        {
            animator.SetInteger("transition", (int) animationType.Idle);
            Debug.Log("idle "+ animator.GetInteger("transition"));
        }
    }

    void animateToRun()
    {
        if (this.countJump == 0)
        {
            animator.SetInteger("transition", (int) animationType.Run);
            Debug.Log("run");
        }
    }

    void animateToJump()
    {
        animator.SetInteger("transition", (int) animationType.Jump);
        Debug.Log("jump");
    }

}

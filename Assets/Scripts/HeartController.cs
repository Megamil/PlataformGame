using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{

    int healthValue;

    private void Awake()
    {
        this.healthValue = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().incrementLife(this.healthValue);
            Destroy(gameObject);
        }
    }

}

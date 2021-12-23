using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    int value;

    private void Awake()
    {
        this.value = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D -> CoinScript");
        if (collision.gameObject.tag == "Player")
        {
            UIController.instance.incrementCoin(this.value);
            Destroy(gameObject);
        }
    }

}

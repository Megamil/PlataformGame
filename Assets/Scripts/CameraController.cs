using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Transform player;
    float smooth;


    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().transform;
        smooth = 5;
    }

    private void LateUpdate()
    {

        if(this.player.position.x >= 0 && this.player.position.x <= 20)
        {
            Vector3 following = new Vector3(player.position.x, this.transform.position.y, this.transform.position.z);
            transform.position = Vector3.Lerp(this.transform.position, following, smooth * Time.deltaTime);
        }

    }
}

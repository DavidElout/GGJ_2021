using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallVisibilityToggle : MonoBehaviour
{

    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (player)
        {
            if (player.transform.position.x != transform.position.x)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    GetComponent<MeshRenderer>().enabled = true;
                }
                else if (player.transform.position.x > transform.position.x)
                {
                    GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }
}

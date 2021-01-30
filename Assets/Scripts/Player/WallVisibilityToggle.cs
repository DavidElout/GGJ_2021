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
        if (player.transform.position.z != transform.position.z)
        {
            if (player.transform.position.z < transform.position.z)
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
            else if (player.transform.position.z > transform.position.z)
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}

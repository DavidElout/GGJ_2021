using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeActivation : MonoBehaviour
{
    public GameObject maze;
    public GameObject corridor;
    public GameObject timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            corridor.SetActive(false);
            maze.SetActive(true);
            timer.SetActive(true);
            // gameObject.SetActive(false);
        }
    }
}

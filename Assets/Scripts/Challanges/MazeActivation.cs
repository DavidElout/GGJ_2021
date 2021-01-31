using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeActivation : MonoBehaviour
{
    public GameObject maze;
    public GameObject timer;
    public GameObject corridor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            maze.SetActive(true);
            timer.SetActive(true);
            corridor.SetActive(false);
        }
    }
}

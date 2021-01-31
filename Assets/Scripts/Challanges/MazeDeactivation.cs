using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDeactivation : MonoBehaviour
{
    public GameObject timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}

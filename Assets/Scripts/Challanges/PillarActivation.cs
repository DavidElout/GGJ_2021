using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PillarActivation : MonoBehaviour
{
    public BossDoorActivation door;
    public BoxCollider trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.UpdateCount();
            trigger.enabled = false;
            // add particles
        }
    }
}

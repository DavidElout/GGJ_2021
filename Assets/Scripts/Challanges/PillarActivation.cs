using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PillarActivation : MonoBehaviour
{
    public BossDoorActivation door;
    public BoxCollider trigger;
    public GameObject flame;
    public GameObject flameLight;
    public AudioClip lit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.UpdateCount();
            trigger.enabled = false;
            // add particles
            flame.SetActive(true);
            flameLight.SetActive(true);
            EazySoundManager.PlaySound(lit, 0.6f);
        }
    }
}

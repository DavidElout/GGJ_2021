using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDeactivation : MonoBehaviour
{
    public GameObject timer;
    public AudioClip music;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer.SetActive(false);
            gameObject.SetActive(false);
            EazySoundManager.PrepareMusic(music, .6f, true, false);
            EazySoundManager.PlayMusic(music, .6f, true, false);
        }
    }
}

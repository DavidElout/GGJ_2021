using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeActivation : MonoBehaviour
{
    public GameObject maze;
    public GameObject timer;
    public GameObject corridor;
    public AudioClip mazeSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            maze.SetActive(true);
            timer.SetActive(true);
            corridor.SetActive(false);
            EazySoundManager.PrepareMusic(mazeSound, .6f, true, false);
            EazySoundManager.PlayMusic(mazeSound, .6f, true, false);
        }
    }
}

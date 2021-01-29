using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    [SerializeField] private bool startOnPlay = false;
    [SerializeField] private ParticleSystem[] particles;
 

    private void Start()
    {
        if (startOnPlay)
        {
            ActivateParticles();
        }
    }


    public void ActivateParticles()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Play();
        }
    }
}

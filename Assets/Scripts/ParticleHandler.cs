using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public void StopParticles()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    public bool IsParticleDone()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            if (particles[i].IsAlive())
                return false;
        }
        return true;
    }


    public void ScaleParticles(float size)
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].transform.DOScale(new Vector3(size, size, size), 1);
        }
    }
}

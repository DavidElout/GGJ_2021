using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollectionPoint : MonoBehaviour, IFlamable
{
    public bool OnFire { get; set; }
    public int SanityPool { get; set; }
    public bool BurnedOut { get; set; }

    [SerializeField] private bool burnEffectConstant;
    [SerializeField] private int startSanityPool;

    private void OnEnable()
    {
        SanityPool = startSanityPool;
    }

    private void BurnFromPool()
    {
        // Add extra effect for giving extra sanity
        // Give fire effect pulsating effect
        SanityPool--;

        if (SanityPool <= 0)
            BurnOut();
    }

    public void Ignite()
    {
        if (!OnFire)
        {
            // Start particle effect
            print("Object started burning");
            OnFire = true;
        }
        BurnFromPool();
    }

    public void Extinguish()
    {
        if (!burnEffectConstant)
        {
            // Stop particle effect
            print("Object stopped burning");
            OnFire = false;
        }
    }

    public void BurnOut()
    {
        print("Object burned out");
        // Burn out the object
        // Let the particle effect burn out
        Destroy(gameObject);
    }
}

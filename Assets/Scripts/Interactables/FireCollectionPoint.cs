using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireCollectionPoint : MonoBehaviour, IFlamable
{
    public bool OnFire { get; set; }
    public int SanityPool { get; set; }
    public bool BurnedOut { get; set; }
    public int TimeToBurnPerSanity { get => timeToBurnPerSanity; set => timeToBurnPerSanity = value; }
    public bool SanityLimitIncrease { get => sanityLimitIncrease; set => sanityLimitIncrease = value; }
    
    [SerializeField] private bool sanityLimitIncrease;
    [SerializeField] private bool burnEffectConstant;
    [SerializeField] private int timeToBurnPerSanity;
    [SerializeField] private int startSanityPool;
    [SerializeField] private ParticleHandler particles;

    private Collider collider;
    private MeshRenderer renderer;

    Coroutine stoppingRoutine;

    private void OnEnable()
    {
        collider = GetComponent<Collider>();
        renderer = GetComponent<MeshRenderer>();
        SanityPool = startSanityPool;
        if(burnEffectConstant)
        {
            if (particles)
                particles.ActivateParticles();
        }
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
            print("Object started burning");
            if(particles)
                particles.ActivateParticles();
            OnFire = true;
        }
        BurnFromPool();
    }

    public void Extinguish()
    {
        if (!burnEffectConstant && OnFire)
        {
            print("Object stopped burning");
            if (particles)
                particles.StopParticles();
            OnFire = false;
        }
    }

    public void BurnOut()
    {
        print("Object burned out");
        BurnedOut = true;
        collider.enabled = false;
        transform.DOScale(Vector3.zero, .3f).Play();
        if (stoppingRoutine == null)
            stoppingRoutine = StartCoroutine(WaitForBurnOut());
    }

    public IEnumerator WaitForBurnOut()
    {
        if (particles)
        {
            particles.StopParticles();
            yield return new WaitUntil(particles.IsParticleDone);
        }
        Destroy(gameObject);
        stoppingRoutine = null;
    }
}

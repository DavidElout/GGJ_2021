using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttack : Attack
{
    [SerializeField] private ParticleHandler particles;
    [SerializeField] private Collider collider;
    [SerializeField] private float shieldTime;
    public bool active;


    public override void DoAttack()
    {
        base.DoAttack();
        StartCoroutine(DoShield());
    }

    public IEnumerator DoShield()
    {
        collider.enabled = true;
        particles.ActivateParticles();
        active = true;
        yield return new WaitForSeconds(shieldTime);
        DeactivateShield();
    }

    public void DeactivateShield()
    {
        collider.enabled = false;
        particles.ForceStopParticles();
        active = false;
        StopAllCoroutines();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (collider.enabled && other.CompareTag("Bullet"))
        {
            DeactivateShield();
        }
    }
}

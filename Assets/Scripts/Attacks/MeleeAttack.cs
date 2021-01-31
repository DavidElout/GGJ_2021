using System.Collections;
using UnityEngine;

public class MeleeAttack : Attack
{
    [SerializeField] private GameObject attackParticle;
    SphereCollider collider;
    ParticleHandler ph;

    public override void DoAttack()
    {
        base.DoAttack();

        collider = attackParticle.GetComponent<SphereCollider>();
        ph = attackParticle.GetComponent<ParticleHandler>();

        collider.enabled = true;
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        ph.ActivateParticles();
        yield return new WaitForSeconds(1);
        collider.enabled = false;
    }
}

using UnityEngine;

public class MeleeAttack : Attack
{
    [SerializeField] private ParticleHandler attackParticle;

    public override void DoAttack()
    {
        base.DoAttack();
        attackParticle.ActivateParticles();
    }
}

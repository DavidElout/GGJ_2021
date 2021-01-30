using UnityEngine;

public class RangedAttack : Attack
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private ParticleHandler attackParticle;

    public override void DoAttack()
    {
        base.DoAttack();
        attackParticle.ActivateParticles();

        if (playerStatus != null) {
            playerStatus.Sanity -= this.Damage;
        }
    }
}

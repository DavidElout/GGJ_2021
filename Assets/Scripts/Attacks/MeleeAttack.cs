using UnityEngine;

public class MeleeAttack : Attack
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private ParticleHandler attackParticle;

    public override void DoAttack()
    {
        base.DoAttack();
        attackParticle.ActivateParticles();
        Debug.Log(playerStatus.Sanity);
        playerStatus.Sanity -= this.Damage;
    }
}

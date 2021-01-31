using UnityEngine;

public class EnemyMeleeAttack : Attack
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private ParticleSystem particleOnHit;

    public override void DoAttack()
    {
        base.DoAttack();

        particleOnHit.Emit(10);
        playerStatus.Sanity -= this.Damage;
    }
}

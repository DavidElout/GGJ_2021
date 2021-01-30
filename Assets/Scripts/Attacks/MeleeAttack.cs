using UnityEngine;

public class MeleeAttack : Attack
{
    [SerializeField] private PlayerStatus playerStatus = null;
    [SerializeField] private ParticleSystem particleOnHit;

    public override void DoAttack()
    {
        base.DoAttack();

        if (playerStatus != null) {
            particleOnHit.Emit(10);
            playerStatus.Sanity -= this.Damage;
        }
    }
}

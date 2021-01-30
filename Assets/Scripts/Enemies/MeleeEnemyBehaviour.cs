using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehaviour : EnemyBehaviour
{
    [SerializeField] private MeleeAttack attack;

    public override void ChasingState()
    {
        base.ChasingState();
        Vector3 playerPosition = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        Vector3 positionDifference = playerPosition - this.transform.position;
        float differenceDistance = positionDifference.magnitude;
        Vector3 differenceDirection = positionDifference.normalized;

        int radiusForMaxForce = 15;
        int maxForce = 75;

        if (differenceDistance > radiusForMaxForce) {
            positionDifference = Vector3.ClampMagnitude(playerPosition - this.transform.position, radiusForMaxForce);
        }

        float forceRatio = differenceDistance / radiusForMaxForce;
        float thrust = forceRatio * maxForce;
        Vector3 forceVector = differenceDirection * thrust;

        rb.AddForce(forceVector);
    }

    public override void AttackingState(Vector3 targetPosition)
    {
        base.AttackingState(targetPosition);
        attack.TryAttack();
    }
}

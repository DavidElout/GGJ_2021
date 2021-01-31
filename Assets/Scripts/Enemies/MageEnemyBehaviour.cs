using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemyBehaviour : EnemyBehaviour
{
    [SerializeField] private RangedAttack attack;

    public override void ChasingState(GameObject targetObject)
    {
        base.ChasingState(targetObject);
        attack.targetObject = targetObject;
        attack.TryAttack();
    }

    public override void AttackingState(GameObject targetObject)
    {
        base.AttackingState(targetObject);
        Vector3 targetPosition = new Vector3(targetObject.transform.position.x, this.transform.position.y, targetObject.transform.position.z);
        Vector3 positionDifference = targetPosition - this.transform.position;
        Vector3 differenceDirection = positionDifference.normalized;
        float differenceDistance = positionDifference.magnitude;

        int radiusForMaxForce = 15;
        int maxForce = 30;

        if (differenceDistance > radiusForMaxForce) {
            positionDifference = Vector3.ClampMagnitude(targetPosition - this.transform.position, radiusForMaxForce);
        }

        float forceRatio = differenceDistance / radiusForMaxForce;
        float thrust = forceRatio * maxForce;
        Vector3 forceVector = differenceDirection * thrust;

        enemyRigidbody.AddForce(-forceVector);

        attack.targetObject = targetObject;
        attack.TryAttack();
    }
}

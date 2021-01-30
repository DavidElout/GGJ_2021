using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemyBehaviour : EnemyBehaviour
{
    [SerializeField] private MeleeAttack attack;
    [SerializeField] public int enemyHealth { get; set; }

    public override void ChasingState(GameObject targetObject)
    {
        base.ChasingState(targetObject);
        Vector3 targetPosition = new Vector3(targetObject.transform.position.x, this.transform.position.y, targetObject.transform.position.z);
        Vector3 positionDifference = targetPosition - this.transform.position;
        Vector3 differenceDirection = positionDifference.normalized;
        float differenceDistance = positionDifference.magnitude;

        int radiusForMaxForce = 15;
        int maxForce = 75;

        if (differenceDistance > radiusForMaxForce) {
            positionDifference = Vector3.ClampMagnitude(targetPosition - this.transform.position, radiusForMaxForce);
        }

        float forceRatio = differenceDistance / radiusForMaxForce;
        float thrust = forceRatio * maxForce;
        Vector3 forceVector = differenceDirection * thrust;

        enemyRigidbody.AddForce(forceVector);
    }

    public override void AttackingState(GameObject targetObject)
    {
        base.AttackingState(targetObject);
        attack.targetObject = targetObject;
        attack.TryAttack();
    }
}

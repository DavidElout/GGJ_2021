﻿using UnityEngine;

public class ProjectileAttack : Attack
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Rigidbody owner;
    public override void DoAttack()
    {
        base.DoAttack();
        if (owner == null)
            owner = GetComponent<Rigidbody>();

        if (RayCasterTool.DoRaycastFromMouse(out RaycastHit hit))
        {
            Vector3 spawnTowards = hit.point - transform.position;
            SpawnProjectile(spawnTowards, owner.velocity);
        }
    }

    private void SpawnProjectile(Vector3 direction, Vector3 ownerVelocity)
    {
        // Spawn the bullet
        GameObject bulletObj = Instantiate(prefab,
                transform.position + direction.normalized,
                Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up));

        // Add the velocity of the shooter
        bulletObj.GetComponent<BaseProjectile>().InitialSpeed =
            Vector3.Dot(direction.normalized, ownerVelocity.normalized)
            * ownerVelocity.magnitude;
    }
}
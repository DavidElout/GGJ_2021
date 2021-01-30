using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackLighting : Attack
{
    [SerializeField] private float rangeOfAttack = 2;
    [SerializeField] private float waitForAttack = 2;
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private PlayerMovement player;

    public override void DoAttack()
    {
        base.DoAttack();
        StartCoroutine(SpawnLighting());
    }

    private IEnumerator SpawnLighting()
    {
        GameObject obj = Instantiate(attackPrefab, player.transform.position, Quaternion.identity);
        ParticleHandler particle = obj.GetComponentInChildren<ParticleHandler>();
        yield return new WaitForSeconds(waitForAttack);

        SphereCollider collider = obj.GetComponent<SphereCollider>();
        collider.enabled = true;
        collider.radius = rangeOfAttack / 2;

        if(particle)
            yield return new WaitUntil(particle.IsParticleDone);

        Destroy(obj);
    }
}

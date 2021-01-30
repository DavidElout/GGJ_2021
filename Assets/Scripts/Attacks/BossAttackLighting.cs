using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackLighting : Attack
{
    [SerializeField] private float rangeOfAttack = 2;
    [SerializeField] private float waitForAttack = 2;
    [SerializeField] private ParticleHandler particles;
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
        yield return new WaitForSeconds(waitForAttack);
        obj.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(waitForAttack);
        Destroy(obj);
    }
}

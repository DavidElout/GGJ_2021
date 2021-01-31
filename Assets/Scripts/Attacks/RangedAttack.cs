using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : Attack
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private int attackAmount;
    public List<GameObject> activeAttacks = new List<GameObject>();
    public List<float> attackLifetimeTimers = new List<float>();
    private GameObject attackObject;
    private bool activated = false;
    private bool dealingDamage = false;

    public override void DoAttack()
    {
        base.DoAttack();

        while (attackLifetimeTimers.Count != 3)
        {
            attackLifetimeTimers.Add(0);
        }

        activated = true;
    }

    private GameObject SpawnAttack(bool inFrontOfPlayer)
    {
        Vector3 direction = targetObject.transform.position;
        Vector2 newPosition2D = new Vector2(direction.x, direction.z) + Random.insideUnitCircle * 5;
        Vector3 newPosition = new Vector3(newPosition2D.x, targetObject.transform.position.y, newPosition2D.y);

        // Spawn the attack
        attackObject = Instantiate(attackPrefab, newPosition, Quaternion.identity);
        attackObject.GetComponent<ParticleHandler>().ActivateParticles();
        return attackObject;
    }

    private IEnumerator CheckForPlayer()
    {
        if (Vector3.Distance(attackObject.transform.position, targetObject.transform.position) < 1)
        {
            dealingDamage = true;
            playerStatus.Sanity -= 1;
            yield return new WaitForSeconds(0.5f);
            dealingDamage = false;
        }
    }

    private void OnDestroy()
    {
        if (attackObject)
        {
            Destroy(attackObject);
        }
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (activated)
        {
            if (timer > 0)
            {
                AttackAnimation(0, 0.5f);
            }
            if (timer > 0.5f)
            {
                AttackAnimation(1, 1f);
            }
            if (timer > 1f)
            {
                AttackAnimation(2, 1.5f);
            }
        }
    }

    private void AttackAnimation(int index, float timePassed)
    {
        if (activeAttacks.Count == index && timer < timePassed)
        {
            activeAttacks.Add(SpawnAttack(true));
            attackLifetimeTimers[index] = 0;
        }

        attackLifetimeTimers[index] += Time.deltaTime;

        // Animation starts attacking
        if (attackLifetimeTimers[index] > 2 && !dealingDamage)
        {
            StartCoroutine(CheckForPlayer());
        }

        // Animation is over
        if (attackLifetimeTimers[index] > 5)
        {
            Destroy(activeAttacks[index]);
            activeAttacks.RemoveAt(index);
            activated = false;
        }
    }
}

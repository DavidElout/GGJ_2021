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

        while (attackLifetimeTimers.Count != 3) {
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
        if (targetObject)
        {
            if (Vector3.Distance(attackObject.transform.position, targetObject.transform.position) < 1)
            {
                dealingDamage = true;
                playerStatus.Sanity -= 1;
                yield return new WaitForSeconds(0.5f);
                dealingDamage = false;
            }
        }
    }

    private void OnDestroy()
    {
        if (attackObject) {
        Destroy(attackObject);
    }
}

    public void Update()
    {
        timer += Time.deltaTime;

        if (activated) {
            if (timer > 0) {
                if (activeAttacks.Count == 0 && timer < 0.5) {
                    activeAttacks.Add(SpawnAttack(true));
                    attackLifetimeTimers[0] = 0;
                }

                attackLifetimeTimers[0] += Time.deltaTime;

                // Animation starts attacking
                if (attackLifetimeTimers[0] > 2 && !dealingDamage) {
                    StartCoroutine(CheckForPlayer());
                }

                // Animation is over
                if (attackLifetimeTimers[0] > 5) {
                    Destroy(activeAttacks[0]);
                    activeAttacks.RemoveAt(0);
                    activated = false;
                }
            }
            
            if (timer > 0.5f) {
                if (activeAttacks.Count == 1 && timer < 1f) {
                    activeAttacks.Add(SpawnAttack(false));
                    attackLifetimeTimers[1] = 0;
                }
                attackLifetimeTimers[1] += Time.deltaTime;

                // Animation starts attacking
                if (attackLifetimeTimers[1] > 2 && !dealingDamage) {
                    StartCoroutine(CheckForPlayer());
                }

                // Animation is over
                if (attackLifetimeTimers[1] > 5) {
                    Destroy(activeAttacks[1]);
                    activeAttacks.RemoveAt(1);
                    activated = false;
                }
            }
            
            if (timer > 1f) {
                if (activeAttacks.Count == 2 && timer < 1.5f) {
                    activeAttacks.Add(SpawnAttack(false));
                    attackLifetimeTimers[2] = 0;
                }
                attackLifetimeTimers[2] += Time.deltaTime;

                // Animation starts attacking
                if (attackLifetimeTimers[2] > 2 && !dealingDamage) {
                    StartCoroutine(CheckForPlayer());
                }

                // Animation is over
                if (attackLifetimeTimers[2] > 5) {
                    Destroy(activeAttacks[2]);
                    activeAttacks.RemoveAt(2);
                    activated = false;
                }
            }
        }
    }
}

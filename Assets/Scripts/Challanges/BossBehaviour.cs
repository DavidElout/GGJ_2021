using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private bool hasSpawned;
    [SerializeField] private bool health;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float restTime;
    private float attacksBeforeRest, attacksInRow;
    private float attackTimer, restTimer;
    public bool HasSpawned { get => hasSpawned; set => hasSpawned = value; }
    public bool Attacking { get => attacksInRow < attacksBeforeRest; }
    public bool Health { get => health; set => health = value; }

    [SerializeField] private List<Attack> attacks = new List<Attack>();


    private void Update()
    {
        if (HasSpawned)
        {
            if (!Attacking)
            {
                restTimer += Time.deltaTime;
                if (restTimer >= restTime)
                {
                    attacksBeforeRest = Random.Range(4, 8);
                    restTimer = 0;
                    attacksInRow = 0;
                }
            }
            else
            {
                attackTimer += Time.deltaTime;
                if (attackTimer > timeBetweenAttacks)
                    AttackPlayer();
            }
        }
    }

    public void AttackPlayer()
    {
        attackTimer = 0;
        attacksInRow++;
        attacks[Random.Range(0,attacks.Count - 1)].DoAttack();
    }
}

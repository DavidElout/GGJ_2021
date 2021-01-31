using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private enum State { Idle, Chasing, Attacking, Dying };
    private State currentState = State.Idle;

    public bool playerInChaseRange = false;
    public bool playerInAttackRange = false;
    public GameObject targetObject;
    public GameObject deathPrefab;
    public int enemyHealth = 2;

    internal Rigidbody enemyRigidbody;

    public virtual void IdleState() { }
    public virtual void ChasingState(GameObject targetObject) { }
    public virtual void AttackingState(GameObject targetObject) { }
    public virtual void DyingState()
    {
        Object.Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet")) {
            enemyHealth -= 2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            if (playerInChaseRange) {
                playerInAttackRange = true;
            } else {
                targetObject = other.gameObject;
                playerInChaseRange = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            if (playerInAttackRange) {
                playerInAttackRange = false;
            } else {
                playerInChaseRange = false;
            }
        }
    }

    private void OnDestroy()
    {
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
    }

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerInChaseRange) {
            if (playerInAttackRange) {
                currentState = State.Attacking;
            } else {
                currentState = State.Chasing;
            }
        } else {
            currentState = State.Idle;
        }

        if (enemyHealth < 1) {
            currentState = State.Dying;
        }

        switch (currentState) {
            case State.Idle:
                IdleState();
                break;
            case State.Chasing:
                ChasingState(targetObject);
                break;
            case State.Attacking:
                AttackingState(targetObject);
                break;
            case State.Dying:
                DyingState();
                break;
        }
    }
}
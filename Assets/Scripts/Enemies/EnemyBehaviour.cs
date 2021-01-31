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

    internal Rigidbody enemyRigidbody;

    public virtual void IdleState() { }
    public virtual void ChasingState(GameObject targetObject) { }
    public virtual void AttackingState(GameObject targetObject) { }
    public virtual void DyingState()
    {
        Object.Destroy(this.gameObject);
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
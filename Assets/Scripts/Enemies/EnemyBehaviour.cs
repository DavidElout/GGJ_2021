using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private enum State { Idle, Chasing, Attacking, Dying };
    private State currentState;

    [SerializeField] private bool playerDetected = false;
    [SerializeField] private bool playerInAttackRange = false;

    public Rigidbody rb;
    public GameObject player;

    IEnumerator IdleState()
    {
        yield return new WaitForSeconds(0.5f);
    }

    public virtual void ChasingState()
    { 

    }
    public virtual void AttackingState(Vector3 targetPosition)
    {

    }

    void DyingState()
    {
        Object.Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            if (!playerDetected) {
                player = other.gameObject;
                playerDetected = true;
            } else {
                playerInAttackRange = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            if (playerInAttackRange) {
                playerInAttackRange = false;
            } else {
                playerDetected = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = State.Idle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerDetected) {
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
                StartCoroutine(IdleState());
                break;
            case State.Chasing:
                ChasingState();
                break;
            case State.Attacking:
                AttackingState(player.transform.position);
                break;
            case State.Dying:
                DyingState();
                break;
        }
    }
}
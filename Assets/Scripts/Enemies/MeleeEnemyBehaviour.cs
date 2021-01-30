using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehaviour : MonoBehaviour
{
    enum State { Idle, Chasing, Attacking, Dying };
    State currentState;

    [SerializeField] private MeleeAttack meleeAttack;
    [SerializeField]  private bool playerDetected = false;
    [SerializeField]  private bool playerInAttackRange = false;

    Rigidbody rb;
    GameObject player;

    IEnumerator IdleState()
    {
        yield return new WaitForSeconds(0.5f);
    }

    void ChasingState()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        Vector3 positionDifference = playerPosition - this.transform.position;
        float differenceDistance = positionDifference.magnitude;
        Vector3 differenceDirection = positionDifference.normalized;

        int radiusForMaxForce = 15;
        int maxForce = 75;

        if (differenceDistance > radiusForMaxForce) {
            positionDifference = Vector3.ClampMagnitude(playerPosition - this.transform.position, radiusForMaxForce);
        }

        float forceRatio = differenceDistance / radiusForMaxForce;
        float thrust = forceRatio * maxForce;
        Vector3 forceVector = differenceDirection * thrust;

        rb.AddForce(forceVector);
    }

    void AttackingState()
    {
        meleeAttack.TryAttack();
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
                AttackingState();
                break;
            case State.Dying:
                DyingState();
                break;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehaviour : MonoBehaviour
{
    enum State { Idle, Chasing, Attacking, Dying };
    State currentState;
    public bool playerDetected = false;
    public bool touchingPlayer = false;
    bool movingEnabled = true;
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

        int radiusForMaxForce = 50;
        int maxForce = 50;

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
        // Attack player.
    }

    void DyingState()
    {
        Object.Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            player = other.gameObject;
            playerDetected = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            playerDetected = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        movingEnabled = false;
    }

    void OnCollisionExit(Collision collision)
    {
        movingEnabled = true;
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
            if (touchingPlayer) {
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
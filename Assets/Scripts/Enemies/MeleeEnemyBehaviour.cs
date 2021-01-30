using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehaviour : MonoBehaviour
{
    enum State { Idle, Chasing, Attacking, Dying };
    State currentState;
    public bool playerDetected = false;
    public bool touchingPlayer = false;
    GameObject player;

    IEnumerator IdleState()
    {
        yield return new WaitForSeconds(0.5f);
    }

    void ChasingState()
    {
        float step = 6 * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, step);
    }

    void AttackingState()
    {
        /* while (true) {
             bool touchingPlayer = false;
             Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1);
             foreach (Collider collider in colliders) {
                 if (collider.gameObject.tag == "Player") {
                      touchingPlayer = true;
                 }
             }

             if (!touchingPlayer) {
                 SetState(playerDetected ? State.Chasing : State.Idle);
                 break;
             }
         }*/
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


    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Idle;
    }

    // Update is called once per frame
    void Update()
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
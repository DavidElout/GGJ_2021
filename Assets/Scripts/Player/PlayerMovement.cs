using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float drag;
    [SerializeField] private Rigidbody rb;

    private Vector3[] directions = { new Vector3(1, 0, 0), new Vector3(0, 0, 1) };

    public Vector3 Velocity => rb.velocity;

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        Vector3 movement = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
        {
            movement = directions[0] * speed;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            movement = directions[0] * -speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement += directions[1] * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement += directions[1] * -speed;
        }

        MovePlayer(movement);
    }

    private void MovePlayer(Vector3 movement)
    {
        rb.AddForce(movement);

        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;

        rb.velocity *= drag;
    }
}

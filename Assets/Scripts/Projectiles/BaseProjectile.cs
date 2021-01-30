using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    public float InitialSpeed { get; set; }

    protected virtual void Update()
    {
        transform.position += transform.forward * (speed + InitialSpeed) * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Destroy(gameObject);
    }
}

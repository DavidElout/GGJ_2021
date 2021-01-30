using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    public int Damage { get; set; }
    public float InitialSpeed { get; set; }
    public bool HitSomething { get; set; }

    protected virtual void Update()
    {
        if(!HitSomething)
            transform.position += transform.forward * (speed + InitialSpeed) * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            StartCoroutine(BurnedOut());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(BurnedOut());
    }

    protected IEnumerator BurnedOut()
    {
        HitSomething = true;
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}

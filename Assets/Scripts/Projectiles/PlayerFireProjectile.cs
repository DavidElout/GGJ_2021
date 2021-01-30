using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireProjectile : BaseProjectile
{
    [SerializeField] private bool weakenOverTime;
    [SerializeField] private float timeToWeaken;
    private float timer;

    protected override void Update()
    {
        base.Update();
        if (weakenOverTime) {
            timer += Time.deltaTime;
            if (timer >= timeToWeaken)
                StartCoroutine(BurnedOut());
        }
    }
}

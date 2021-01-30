﻿using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float cooldownTime;
    private float timer = 0;

    [SerializeField] private int damage = 0;
    public float Range { get; set; }
    public int Damage => damage;


    private void Update()
    {
        timer += Time.deltaTime;
    }

    public bool TryAttack()
    {
        if (timer > cooldownTime)
        {
            DoAttack();
            timer = 0;
            return true;
        }
        return false;
    }

    public virtual void DoAttack()
    {
    }
}

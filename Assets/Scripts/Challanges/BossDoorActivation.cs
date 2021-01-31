using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorActivation : MonoBehaviour
{
    public int targetCount = 3;
    public BoxCollider trigger;

    private int count = 0;

    public void UpdateCount()
    {
        count++;
        if (count == targetCount)
        {
            trigger.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}

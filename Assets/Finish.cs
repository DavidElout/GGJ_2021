using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject boink;

    public void OnTriggerEnter(Collider other)
    {
        boink.SetActive(true);
    }
}

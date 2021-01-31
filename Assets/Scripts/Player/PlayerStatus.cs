using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "ScriptableObjects/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    public event EventHandler<int> SanityChangedEvt;
    public event EventHandler<int> SanityLimitChangedEvt;

    public AudioClip loseHealth;
    public AudioClip increaseHealth;


    [Header("Runtime values (Don't change)")]
    [SerializeField] private int sanity;
    [SerializeField] private Vector2Int sanityLimit;

    [Header("Starting values")]
    [SerializeField] private int sanityStartingValue;
    [SerializeField] private int sanityUpperLimitStartingValue;

    public int Sanity 
    { 
        get => sanity; 
        set 
        {
            if (value > sanity)
                EazySoundManager.PlaySound(increaseHealth);
            else
                EazySoundManager.PlaySound(loseHealth);
            sanity = Mathf.Clamp(value, sanityLimit.x, sanityLimit.y);
            SanityChangedEvt?.Invoke(this, sanity); 
        } 
    }
    public Vector2Int SanityLimit {
        get => sanityLimit;
        set
        {
            sanityLimit = value;
            SanityLimitChangedEvt?.Invoke(this, sanityLimit.y);
        }
    }
    public float SanityPercentage { get => (sanity / (float)sanityLimit.y); }
    
    public void SanityReset()
    {
        SanityLimit = new Vector2Int(sanityLimit.x, sanityUpperLimitStartingValue);
        Sanity = sanityStartingValue;
    }

    public void IncreaseSanityLimit(int amount)
    {
        SanityLimit = new Vector2Int(sanityLimit.x, sanityLimit.y += amount);
    }
}

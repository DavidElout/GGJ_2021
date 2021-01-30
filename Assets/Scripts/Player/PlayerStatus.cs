using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "ScriptableObjects/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    public event EventHandler<int> SanityChangedEvt;
    public event EventHandler<int> SanityLimitChangedEvt;

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
        sanity = sanityStartingValue;
        sanityLimit.y = sanityUpperLimitStartingValue;
    }
    
    public void IncreaseSanityLimit(int amount)
    {
        sanityLimit.y += amount;
    }
}

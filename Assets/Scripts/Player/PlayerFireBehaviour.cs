using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;

    private float timer = 0, timeUntilFireEffect = 2;
    private IFlamable currentFlamableObject;


    private void Awake()
    {
        playerStatus.SanityReset();
        playerStatus.SanityChangedEvt += PlayerStatus_SanityChangedEvt;
    }

    private void OnDestroy()
    {
        playerStatus.SanityChangedEvt -= PlayerStatus_SanityChangedEvt;
    }

    private void PlayerStatus_SanityChangedEvt(object sender, int e)
    {
        transform.localScale = Vector3.one * e;
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootFire();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse2))
        {
            MeleeAttack();
        }
    }

    private void ShootFire()
    {
        playerStatus.Sanity--;
    }

    private void MeleeAttack()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        /*if(other.CompareTag("Bullet"))
        {
            playerStatus.Sanity--;
        }   */
        if(other.CompareTag("FireSource"))
        {
            timer += Time.deltaTime;

            if(timer > timeUntilFireEffect)
            {
                currentFlamableObject = other.GetComponent<IFlamable>();
                timer = 0;
                currentFlamableObject.Ignite();
                playerStatus.Sanity++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FireSource"))
        {
            IFlamable flamableObject = other.GetComponent<IFlamable>();
            flamableObject.Extinguish();
            timer = 0;
        }
    }
}

public interface IFlamable
{
    int SanityPool { get; set; } // Amount of sanity increase before burning out
    bool OnFire { get; set; }
    bool BurnedOut { get; set; }
    void Ignite();
    void Extinguish();
}

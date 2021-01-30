using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Light playerLight;
    [SerializeField] private ParticleHandler playerParticles;

    [SerializeField] private MeleeAttack meleeAttack;
    [SerializeField] private ProjectileAttack projectileAttack;


    private float timer = 0, timeUntilFireEffect = 2;
    private IFlamable currentFlamableObject;

    private Sequence flickerSequence;

    private void Awake()
    {
        playerStatus.SanityChangedEvt += PlayerStatus_SanityChangedEvt;
        playerStatus.SanityReset();

        flickerSequence = DOTween.Sequence();
    }

    private void OnDestroy()
    {
        playerStatus.SanityChangedEvt -= PlayerStatus_SanityChangedEvt;
    }

    private void PlayerStatus_SanityChangedEvt(object sender, int e)
    {
        playerParticles.ScaleParticles(e / 10f + 0.4f);
    }

    void Update()
    {
        HandleInput();
        
        if(!flickerSequence.active)
            flickerSequence.Append(playerLight.DOIntensity(Random.Range(0f, 2f), Random.Range(.2f, 1f)));
        playerLight.range = playerLight.intensity * 50;
    }


    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootFire();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            meleeAttack.TryAttack();
        }
    }

    private void ShootFire()
    {
        if (playerStatus.Sanity > 1)
        {
            if(projectileAttack.TryAttack())
                playerStatus.Sanity--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) {
            playerStatus.Sanity -= other.GetComponent<BaseProjectile>().Damage;
        }
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("FireSource"))
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

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private Light playerLight;
    [SerializeField] private ParticleHandler particles;

    [SerializeField] private GameObject bulletPrefab;

    private float timer = 0, timeUntilFireEffect = 2;
    private IFlamable currentFlamableObject;

    private Sequence flickerSequence;

    private void Awake()
    {
        playerStatus.SanityReset();
        playerStatus.SanityChangedEvt += PlayerStatus_SanityChangedEvt;

        flickerSequence = DOTween.Sequence();
    }

    private void OnDestroy()
    {
        playerStatus.SanityChangedEvt -= PlayerStatus_SanityChangedEvt;
    }

    private void PlayerStatus_SanityChangedEvt(object sender, int e)
    {
        particles.ScaleParticles(e / 4f);
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
        else if(Input.GetKeyDown(KeyCode.Mouse2))
        {
            MeleeAttack();
        }
    }

    private void ShootFire()
    {
        if (playerStatus.Sanity > 1)
        {
            if (RayCasterTool.DoRaycastFromMouse(out RaycastHit hit))
            {
                playerStatus.Sanity--;
                Vector3 spawnTowards = hit.point - transform.position;
                Instantiate(bulletPrefab, 
                    transform.position + spawnTowards.normalized, 
                    Quaternion.LookRotation(new Vector3(spawnTowards.x, 0, spawnTowards.z), Vector3.up));
            }
        }
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

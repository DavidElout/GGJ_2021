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
    [SerializeField] private ShieldAttack shieldAttack;

    [Header("Sanity behaviour")]
    [SerializeField] private int lightIntensityRangeEffect = 20;
    [SerializeField] private float lightIntensityAffect = 0.2f;
    [SerializeField] private float lightIntensityBaseValue = 0.5f;

    [SerializeField] private float flameScaleAffect = 0.1f;
    [SerializeField] private float flameScaleBaseValue = 0.5f;

    [SerializeField] private float currentLightIntensity;

    [Header("Sanity effects")]
    [SerializeField] Material neonWallMat;

    private float burnTimer = 0;
    private IFlamable currentFlamableObject;

    private Sequence flickerSequence;
    private Sequence lowHealthSequence;

    public MeleeAttack MeleeAttack => meleeAttack;
    public ProjectileAttack ProjectileAttack => projectileAttack;
    public ShieldAttack ShieldAttack => shieldAttack;

    private void Start()
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
        playerParticles.ScaleParticles(e * flameScaleAffect + flameScaleBaseValue);
        currentLightIntensity = e * lightIntensityAffect + lightIntensityBaseValue;
    }

    void Update()
    {
        HandleInput();

        if (flickerSequence != null)
        {
            if (!flickerSequence.active)
                flickerSequence.Append(playerLight.DOIntensity(Random.Range(currentLightIntensity - 1f, currentLightIntensity + 1f), Random.Range(.2f, 1f)));
        }
        playerLight.range = playerLight.intensity * lightIntensityRangeEffect;

        ShowHealth();
    }

    private void ShowHealth()
    {
        if (playerStatus.Sanity <= 2)
        {
            if (lowHealthSequence == null)
            {
                lowHealthSequence = DOTween.Sequence();
                lowHealthSequence.SetLoops(-1);
                lowHealthSequence.Append(neonWallMat.DOColor(new Color(1, 0, 0), .6f));
                lowHealthSequence.Append(neonWallMat.DOColor(new Color(.5f, 0, 0), .6f));
            }
        }
        else
        {
            neonWallMat.color = new Color(1 - playerStatus.SanityPercentage, playerStatus.SanityPercentage, 0);
            lowHealthSequence.Kill();
            lowHealthSequence = null;
        }
        if (playerStatus.Sanity <= 0)
            Die();
    }

    private void Die()
    {
        DeathMenu.Instance.ToggleMenu(true);
        // Do death animation
        Destroy(gameObject);
    }

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            ShootFire();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            meleeAttack.TryAttack();
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            Shield();
        }
    }

    private void Shield()
    {
        if (playerStatus.Sanity > 1)
        {
            if(shieldAttack.TryAttack())
                playerStatus.Sanity--;
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
        if (!shieldAttack.active)
        {
            if (other.CompareTag("Bullet"))
            {
                playerStatus.Sanity -= other.GetComponent<BaseProjectile>().Damage;
            }
        }
        if (other.CompareTag("FireSource"))
        {
            if (currentFlamableObject == null || currentFlamableObject.BurnedOut)
                currentFlamableObject = other.GetComponent<IFlamable>();

            burnTimer = currentFlamableObject.TimeToBurnPerSanity;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FireSource"))
        {
            if(currentFlamableObject == null || currentFlamableObject.BurnedOut)
                currentFlamableObject = other.GetComponent<IFlamable>();

            if (burnTimer > currentFlamableObject.TimeToBurnPerSanity)
            {
                burnTimer = 0;
                currentFlamableObject.Ignite();
                playerStatus.Sanity++;
            }

            burnTimer += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FireSource"))
        {
            currentFlamableObject = null;
            IFlamable flamableObject = other.GetComponent<IFlamable>();
            flamableObject.Extinguish();
            burnTimer = 0;
        }
    }
}

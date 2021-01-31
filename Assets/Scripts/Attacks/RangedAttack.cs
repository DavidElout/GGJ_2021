using UnityEngine;
using System.Collections;

public class RangedAttack : Attack
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private GameObject attackPrefab;
    private GameObject attackObject;
    private bool activated = false;
    private bool dealingDamage = false;
    public float animationTimer = 0;
    
    public override void DoAttack()
    {
        base.DoAttack();
        SpawnAttack();
        activated = true;
    }

    private void SpawnAttack()
    {
        // Spawn the attack
        Vector3 direction = targetObject.transform.position;
        attackObject = Instantiate(attackPrefab, direction, Quaternion.identity);
        attackObject.GetComponent<ParticleHandler>().ActivateParticles();
    }

    private IEnumerator CheckForPlayer()
    {
        if (Vector3.Distance(attackObject.transform.position, targetObject.transform.position) < 1) {
            dealingDamage = true;
            playerStatus.Sanity -= 1;
            yield return new WaitForSeconds(0.5f);
            dealingDamage = false;
        }
    }

    private void OnDestroy()
    {
        if (attackObject)
            Destroy(attackObject);
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (activated) {
            animationTimer += Time.deltaTime;
            // aniamtion starts attacking
            if (animationTimer > 2 && !dealingDamage) {
                StartCoroutine(CheckForPlayer());
            }

            // aniamtion is over
            if (animationTimer > 5) {
                Destroy(attackObject);
                activated = false;
                animationTimer = 0;
            }
        }
    }
}

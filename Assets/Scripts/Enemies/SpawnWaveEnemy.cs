using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaveEnemy : MonoBehaviour
{
    public enum EnemyType { SimpleMelee, Tank, Ranged, Mage };
    public EnemyType selectedEnemyType;
    public int spawnAmount = 1;
    public int spawnTimes = 1;
    public float spawnDelayInMilliseconds = 500.0f;
    public float spawnRadius = 1.0f;

    public bool spawnEnemies = true;

    public List<int> waveAmount = new List<int>();
    public bool WavesCompleted => wavesCompleted;


    GameObject enemyObject;
    private int currentWaveIndex = 0;
    private int spawnedEnemies = 0;
    private bool wavesCompleted = false;

    Vector3 GetValidRandomLocation(int count = 0)
    {
        count++;
        Vector3 randomCoordinates = new Vector3((Random.Range(0, spawnRadius) - spawnRadius / 2) + this.transform.position.x, 0.5f, (Random.Range(0, spawnRadius) - spawnRadius / 2) + this.transform.position.z);
        bool coordinatesValid = true;

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, spawnRadius);
        foreach (Collider collider in colliders) {
            if (collider.gameObject.tag == "Enemy") {
                Vector3 colliderCoordinates = collider.gameObject.transform.position;
                Vector3 colliderScale = collider.gameObject.transform.localScale;

                if (collider.bounds.Contains(randomCoordinates)) {
                    coordinatesValid = false;
                }
            }
        }

        if (count > 9 || coordinatesValid) {
            return randomCoordinates;
        } else {
            return GetValidRandomLocation(count);
        }
    }

    GameObject CreateEnemyObject(EnemyType enemyType)
    {
        GameObject enemyObject = null;
        switch (enemyType) {
            case EnemyType.SimpleMelee:
                enemyObject = Resources.Load<GameObject>("Prefabs/Simple Melee Enemy");
                break;
            case EnemyType.Tank:
                enemyObject = Resources.Load<GameObject>("Prefabs/Ranged Enemy");
                break;
            case EnemyType.Ranged:
                enemyObject = Resources.Load<GameObject>("Prefabs/Ranged Enemy");
                break;
            case EnemyType.Mage:
                enemyObject = Resources.Load<GameObject>("Prefabs/Simple Melee Enemy");
                break;
        }
        return enemyObject;
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < spawnTimes; i++) {
            for (int j = 0; j < spawnAmount; j++) {
                GameObject enemyObject = CreateEnemyObject(selectedEnemyType);
                spawnedEnemies++;
                Instantiate(enemyObject, GetValidRandomLocation(), Quaternion.identity);
                yield return new WaitForSeconds(spawnDelayInMilliseconds / 1000);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnEnemies && spawnedEnemies == 0) {
            spawnAmount = waveAmount[currentWaveIndex];
            StartCoroutine(SpawnEnemies());
            spawnEnemies = false;
            currentWaveIndex++;
            if (currentWaveIndex >= waveAmount.Count)
            {
                wavesCompleted = true;
            }
        }
    }
}

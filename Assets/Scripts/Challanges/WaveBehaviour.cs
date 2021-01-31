using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour
{
    [SerializeField] private List<SpawnWaveEnemy> enemyWaveSpawners;
    [SerializeField] private GameObject door;
    [SerializeField] private List<GameObject> blockades;

    private int spawnerAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (SpawnWaveEnemy enemyWaveSpawner in enemyWaveSpawners)
        {
            if (enemyWaveSpawner.WavesCompleted)
                spawnerAmount++;
            if (spawnerAmount == enemyWaveSpawners.Count)
                CompletedWaves();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (SpawnWaveEnemy enemyWaveSpawner in enemyWaveSpawners)
            {
                enemyWaveSpawner.spawnEnemies = true;
            }
            foreach (GameObject blockade in blockades)
            {
                blockade.SetActive(true);
            }
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void CompletedWaves()
    {
        Debug.Log("comp waves?");
        door.SetActive(false);
        foreach (SpawnWaveEnemy enemyWaveSpawner in enemyWaveSpawners)
        {
            enemyWaveSpawner.spawnEnemies = true;
        }
        foreach (GameObject blockade in blockades)
        {
            blockade.SetActive(false);
        }
    }
}

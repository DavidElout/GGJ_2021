using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour
{
    [SerializeField] private SpawnWaveEnemy enemyWaveSpawner;
    [SerializeField] private GameObject door;
    [SerializeField] private List<GameObject> blockades;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyWaveSpawner.WavesCompleted)
            CompletedWaves();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyWaveSpawner.spawnEnemies = true;
            foreach (GameObject blockade in blockades)
            {
                blockade.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }

    private void CompletedWaves()
    {
        door.SetActive(false);
        enemyWaveSpawner.spawnEnemies = false;
        foreach (GameObject blockade in blockades)
        {
            blockade.SetActive(false);
        }
    }
}

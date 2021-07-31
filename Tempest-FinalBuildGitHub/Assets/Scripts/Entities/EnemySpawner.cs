using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject straightEnemyPrefab;
    public GameObject hopperEnemyPrefab;
    public GameObject spawnEnemyPrefab;
    public GameObject spikerEnemyPrefab;

    [SerializeField]
    private GameObject mapManagerObject;
    private MapManager mapManager;

    [SerializeField]
    private GameObject enemyHolderObject;

    // Start is called before the first frame update
    void Awake()
    {
        mapManager = mapManagerObject.GetComponent<MapManager>();
    }

    public void SpawnStraight() {
        GameObject enemy = Instantiate(straightEnemyPrefab, Vector3.zero, Quaternion.identity, enemyHolderObject.transform);
        enemy.GetComponent<StraightEnemyController>().GoTo(Random.Range(0, mapManager.planes.Count));
        LevelManager.Instance.enemyCount += 1;
    }

    public void SpawnTank() {
        GameObject enemy = Instantiate(spawnEnemyPrefab, Vector3.zero, Quaternion.identity, enemyHolderObject.transform);
        enemy.GetComponent<SpawnEnemyController>().GoTo(Random.Range(0, mapManager.planes.Count));
        LevelManager.Instance.enemyCount += 1;
    }

    public void SpawnHopper()
    {
        GameObject enemy = Instantiate(hopperEnemyPrefab, Vector3.zero, Quaternion.identity, enemyHolderObject.transform);
        enemy.GetComponent<HopperEnemyController>().GoTo(Random.Range(0, mapManager.planes.Count));
        LevelManager.Instance.enemyCount += 1;
    }

    public void SpawnSpikers(float probability = 0.6f)
    {
        for (int i = 0; i < mapManager.planes.Count; i++) {
            if (Random.value > probability)
            {
                mapManager.spikeMap.Add(true);
                GameObject enemy = Instantiate(spikerEnemyPrefab, Vector3.zero, Quaternion.identity, enemyHolderObject.transform);
                mapManager.spikes.Add(enemy);

                enemy.GetComponent<SpikerEnemyController>().GoTo(i);
                LevelManager.Instance.enemyCount += 1;
            }
            else {
                mapManager.spikes.Add(null);
                mapManager.spikeMap.Add(false);
            }
        }
    }
}

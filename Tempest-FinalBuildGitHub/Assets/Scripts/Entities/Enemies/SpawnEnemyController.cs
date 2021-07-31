using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyController : BaseEnemyController
{
    public GameObject enemyPrefab;
    public float speed = 8f;

    public override void Start()
    {
        base.Start();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            SplitEnemy();
            Destroy(collider.gameObject);
        }
    }

    void Update()
    {
        if (transform.position.z < mapManager.GetPlaneTransform(0, 1).position.z)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        else if (!reachedEdge)
        {
            reachedEdge = true;
            SplitEnemy();
        }
    }

    private void SplitEnemy()
    {
        EffectsManager.instance.playExp2();

        HopperEnemyController enemy1 = Instantiate(enemyPrefab, transform.position, transform.rotation, transform.parent).GetComponent<HopperEnemyController>();
        HopperEnemyController enemy2 = Instantiate(enemyPrefab, transform.position, transform.rotation, transform.parent).GetComponent<HopperEnemyController>();

        enemy1.GoTo(objectLocation, true);
        enemy2.GoTo(objectLocation, true);

        enemy1.StartCoroutine(enemy1.Lerp(enemy1.IncrementLocationPreserveZ(1), 0.1f));
        enemy2.StartCoroutine(enemy2.Lerp(enemy2.IncrementLocationPreserveZ(-1), 0.1f));

        LevelManager.Instance.enemyCount += 1;
        LevelManager.Instance.UpdateScore(100);

        Destroy(gameObject);
    }
}

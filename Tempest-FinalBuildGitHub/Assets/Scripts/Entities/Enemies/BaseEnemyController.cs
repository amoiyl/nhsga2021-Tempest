using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyController : PlaneObject
{
    public bool reachedEdge = false;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public void GoTo(int location, bool preserveZ = false)
    {
        objectLocation = location;
        TransformData spawnLocation = mapManager.GetPlaneTransform(objectLocation, -1);
        if (preserveZ)
        {
            spawnLocation.position = new Vector3(spawnLocation.position.x, spawnLocation.position.y, transform.position.z);
        }
        spawnLocation.ApplyTo(transform);
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            LevelManager.Instance.enemyCount -= 1;
            LevelManager.Instance.UpdateScore(150);
            EffectsManager.instance.playExp1();
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
}

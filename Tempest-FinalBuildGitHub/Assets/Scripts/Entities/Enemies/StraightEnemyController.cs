using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightEnemyController : BaseEnemyController
{
    private bool isClockwise;

    public float speed = 15f;

    public override void Start()
    {
        base.Start();

        isClockwise = Random.value > 0.5f;
    }

    void Update()
    {
        if (transform.position.z < mapManager.GetPlaneTransform(0, 1).position.z)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        else
        {
            if (!reachedEdge)
            {
                reachedEdge = true;
            }
            if (!isTraveling)
            {
                LoopAround();
            }
        }
    }

    private void LoopAround()
    {
        if (isClockwise)
        {
            StartCoroutine(Lerp(IncrementLocation(-1), 0.3f));
        }
        else
        {
            StartCoroutine(Lerp(IncrementLocation(1), 0.3f));
        }
    }
}

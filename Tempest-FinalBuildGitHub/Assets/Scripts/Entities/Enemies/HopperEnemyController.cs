using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperEnemyController : BaseEnemyController
{
    private bool isClockwise;
    private IEnumerator hopping;
    public float speed = 12f;

    public override void Start()
    {
        base.Start();

        isClockwise = Random.value > 0.5f;

        hopping = Hop();
        StartCoroutine(hopping);
    }

    private IEnumerator Hop()
    {
        while (!reachedEdge) {
            yield return new WaitForSeconds(0.5f);
            int favoredDirection = isClockwise ? 1 : -1;
            yield return StartCoroutine(Lerp(IncrementLocationPreserveZ(Random.value > 0.2 ? favoredDirection : -1 * favoredDirection), 0.3f));
        }
    }

    void Update()
    {
        if (transform.position.z < mapManager.GetPlaneTransform(0, 1).position.z)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        } else {
            if (!reachedEdge) {
                reachedEdge = true;
                Debug.Log("stopping");
                StopCoroutine(hopping);
            }
            if (!isTraveling) {
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

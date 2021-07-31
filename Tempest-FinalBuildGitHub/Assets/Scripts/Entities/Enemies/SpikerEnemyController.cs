using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikerEnemyController : BaseEnemyController
{

    [SerializeField]
    private GameObject tail;

    [SerializeField]
    private GameObject head;

    [SerializeField]
    private GameObject spawnEnemyPrefab;

    private bool movingForward = true;
    private int distance;

    private float startZ;
    private float endZ;

    public float speed = 10f;

    public override void Start()
    {
        base.Start();

        startZ = mapManager.GetPlaneTransform(0, -1).position.z;
        endZ = mapManager.GetPlaneTransform(0, 1).position.z;

        distance = (int) Random.Range(Mathf.Abs(startZ / 2), endZ + Mathf.Abs(startZ / 2));

        mapManager.spikes.Add(this.gameObject);
    }

    void Update()
    {
        if (transform.position.z < startZ + distance && movingForward)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
            TrackTail();
        }
        else if (head != null && head.transform.position.z > mapManager.GetPlaneTransform(0, -1).position.z && !movingForward)
        {
            tail.transform.parent = mapManager.transform;
            head.transform.position += Vector3.back * speed * 3 * Time.deltaTime;
        }
        else if (head != null && head.transform.position.z <= mapManager.GetPlaneTransform(0, -1).position.z && !movingForward)
        {
            Destroy(this.head);
            GameObject enemy = Instantiate(spawnEnemyPrefab, Vector3.zero, Quaternion.identity, transform.parent);
            enemy.GetComponent<SpawnEnemyController>().GoTo(objectLocation);
        }
        else if (movingForward && transform.position.z > startZ + distance)
        {
            tail.transform.parent = mapManager.transform;
            movingForward = false;
        }

        if (distance <= 0) {
            mapManager.spikeMap[objectLocation] = false;

            Destroy(this.tail);
            Destroy(this.head);
            Destroy(this.gameObject);
        }
    }

    void TrackTail() {
        Vector3 tailScale = tail.transform.localScale;
        tailScale.z = transform.position.z - startZ;
        tail.transform.localScale = tailScale;

        Vector3 tailLocation = tail.transform.position;
        tailLocation.z = (transform.position.z + startZ) / 2;
        tail.transform.position = tailLocation;
    }

    void FixTail(float distance) {
        Vector3 tailScale = tail.transform.localScale;
        tailScale.z = distance;
        tail.transform.localScale = tailScale;

        Vector3 tailLocation = tail.transform.position;
        tailLocation.z = startZ + distance / 2;
        tail.transform.position = tailLocation;
    }

    public IEnumerator FullyElongateTail(float duration) {
        Vector3 endScale = new Vector3(tail.transform.localScale.x * 5, tail.transform.localScale.y * 5, endZ - startZ);
        Vector3 endLocation = new Vector3(tail.transform.position.x, tail.transform.position.y, 0);

        float time = 0;
        Vector3 startScale = tail.transform.localScale;
        Vector3 startPosition = tail.transform.localPosition;

        while (time < duration)
        {
            tail.transform.localScale = Vector3.Lerp(startScale, endScale, time / duration);
            tail.transform.localPosition = Vector3.Lerp(startPosition, endLocation, time / duration);

            time += Time.deltaTime;
            yield return null;
        }

        tail.transform.localScale = endScale;
        tail.transform.localPosition = endLocation;
    }

    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            if (head != null) {
                LevelManager.Instance.enemyCount -= 1;

                distance = (int)(transform.position.z - startZ);
                tail.transform.parent = mapManager.transform;

                LevelManager.Instance.UpdateScore(50);
                Destroy(head);
            }
            else {
                if (distance > 0)
                {
                    distance = Mathf.Max(0, distance - 5);
                    LevelManager.Instance.UpdateScore(10);
                    FixTail(distance);
                }
            }

            Destroy(collider.gameObject);
        }
    }
}
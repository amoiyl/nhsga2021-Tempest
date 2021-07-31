using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PlaneObject
{
    private PlayerController playerController;
    private bool isClockwise;
    AudioSource dead;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        isClockwise = Random.value > 0.5f;
        dead = GetComponent<AudioSource>();
    }
     
    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < playerController.gameObject.transform.position.z)
        {
            transform.position += Vector3.forward * 15f * Time.deltaTime;
        }
        else if (!isTraveling)
        {
            EnemyMovement();
        }
    }

    public void GoTo(int location, bool preserveZ = false) {
        objectLocation = location;
        TransformData spawnLocation = mapManager.GetPlaneTransform(objectLocation, -1);
        if (preserveZ) {
            spawnLocation.position = new Vector3(spawnLocation.position.x, spawnLocation.position.y, transform.position.z);
        }
        spawnLocation.ApplyTo(transform);
    }
     
    private void EnemyMovement()
    {
        //GetCounterClockwiseDistance(playerController) < GetClockwiseDistance(playerController)

        if (isClockwise)
        {
            StartCoroutine(Lerp(IncrementLocation(-1), 0.5f));
        }
        else
        {
            StartCoroutine(Lerp(IncrementLocation(1), 0.5f));
        }
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        dead.Play();
        if (collider.gameObject.tag == "Bullet")
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
}

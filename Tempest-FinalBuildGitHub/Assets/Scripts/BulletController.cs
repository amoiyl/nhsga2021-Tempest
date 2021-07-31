using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 15f;
    public int shot;
    private MapManager mapManager;


    void Start()
    {
        shot = 0;
        mapManager = GameObject.FindWithTag("MapManager").GetComponent<MapManager>();
    }
     
    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
        if (transform.position.z < mapManager.GetPlaneTransform(0).position.z - mapManager.plane.transform.localScale.z) 
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public GameObject plane;
    public Camera playerCamera;

    public List<GameObject> planes;
    public List<bool> spikeMap;
    public List<GameObject> spikes;

    public ShapeManager.Shape shape;
    public bool isLoop = true;
    public int selectedLocation;

    private int temp_counter = 0;

    void Awake()
    {
        SetMapShape(4); 
        GenerateShape();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            Debug.Log("e");
            temp_counter += 1;
            foreach (GameObject plane in planes)
            {
                Destroy(plane);
            }

            planes = new List<GameObject>();
            spikeMap = new List<bool>();
            spikes = new List<GameObject>();

            SetMapShape(temp_counter);
            StartCoroutine(GenerateMap());
        }
    }

    public void SetMapShape(int num)
    {
        num = num % 8;
        switch (num)
        {
            case 0:
                shape = ShapeManager.Circle;
                break;
            case 1:
                shape = ShapeManager.Flat;
                break;
            case 2:
                shape = ShapeManager.SlopeLine;
                break;
            case 3:
                shape = ShapeManager.Cshape;
                break;
            case 4:
                shape = ShapeManager.ZigZagshape;
                break;
            case 5:
                shape = ShapeManager.Box;
                break;
            case 6:
                shape = ShapeManager.Triangle;
                break;
            case 7:
                shape = ShapeManager.FlatBox;
                break;
        }
    }

    public void LoadNewMap(int num)
    {
        foreach (GameObject plane in planes)
        {
            Destroy(plane);
        }

        planes = new List<GameObject>();
        spikeMap = new List<bool>();
        spikes = new List<GameObject>();

        SetMapShape(num);
        GenerateShape();
    }

    public void SelectLocationColor(int other)
    {
        planes[selectedLocation].GetComponent<Plane>().SelectColor(false);

        selectedLocation = other;

        planes[selectedLocation].GetComponent<Plane>().SelectColor(true);
    }

    public void GenerateShape()
    {
        //List<float> angles = shape.angles;
        //Plane previousPlane = null;
        //float rotation = 0;
        //foreach (float angle in angles)
        //{
        //    Vector3 spawnLocation = Vector3.zero;
        //    if (previousPlane != null)
        //    {
        //        spawnLocation = previousPlane.GetLeftEdge();
        //    } 
        //    previousPlane = Instantiate(plane, Vector3.zero, Quaternion.identity, this.gameObject.transform).GetComponent<Plane>();
        //    previousPlane.transform.localPosition = spawnLocation;

        //    rotation += angle;
        //    previousPlane.RotateAroundEdge(rotation);
        //    planes.Add(previousPlane.gameObject);
        //}

        //isLoop = shape.isLoop;
        //playerCamera.transform.position = new Vector3(planes[shape.center].transform.position.x + plane.transform.localScale.x / 2, playerCamera.transform.position.y, playerCamera.transform.position.z);

        StartCoroutine(GenerateMap());
    }

    private IEnumerator GenerateMap() {
        List<float> angles = shape.angles;
        Plane previousPlane = null;
        float rotation = 0;
        foreach (float angle in angles)
        {
            Vector3 spawnLocation = Vector3.zero;
            if (previousPlane != null)
            {
                spawnLocation = previousPlane.GetLeftEdge();
            }
            previousPlane = Instantiate(plane, Vector3.zero, Quaternion.identity, this.gameObject.transform).GetComponent<Plane>();
            previousPlane.transform.localPosition = spawnLocation;

            rotation += angle;

            yield return new WaitForSeconds(0.05f);
            previousPlane.RotateAroundEdge(rotation);
            yield return new WaitForSeconds(0.05f);
            planes.Add(previousPlane.gameObject);
        }

        isLoop = shape.isLoop;
        //playerCamera.transform.position = new Vector3(planes[shape.center].transform.position.x + plane.transform.localScale.x / 2, playerCamera.transform.position.y, playerCamera.transform.position.z);
    }

    public TransformData GetPlaneTransform(int playerLocation, float zLocation = 1)
    {
        GameObject currentPlane = planes[playerLocation];
        TransformData planeTransform = new TransformData(currentPlane.gameObject.transform);
        planeTransform.position += plane.transform.localScale.x / 2 * currentPlane.GetComponent<Plane>().GetEulerRotation() + zLocation * Vector3.forward * plane.transform.localScale.z / 2;
        return planeTransform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneObject : MonoBehaviour
{
    public int objectLocation = 0;
    public bool isTraveling = false;

    public MapManager mapManager;

    public void Awake()
    {
        mapManager = GameObject.FindWithTag("MapManager").GetComponent<MapManager>();
    }

    public virtual void Start()
    {
    }

    public int GetClockwiseDistance(PlaneObject otherObject)
    {
        return Mathf.Abs(objectLocation - otherObject.objectLocation);
    }

    public int GetCounterClockwiseDistance(PlaneObject otherObject)
    {
        int planeCount = mapManager.planes.Count;
        return Mathf.Abs(objectLocation + (planeCount - otherObject.objectLocation));
    }

    public void MoveTo(TransformData endLocation)
    {
        transform.position = endLocation.position;
        transform.rotation = endLocation.rotation;
    }

    public TransformData IncrementLocationPreserveZ(int amount)
    {
        objectLocation = GetIncrementLocation(amount);
        TransformData newLocation = mapManager.GetPlaneTransform(objectLocation);
        newLocation.position = new Vector3(newLocation.position.x, newLocation.position.y, transform.position.z);
        return newLocation;
    }

    public TransformData IncrementLocation(int amount)
    {
        objectLocation = GetIncrementLocation(amount);
        return mapManager.GetPlaneTransform(objectLocation);
    }

    public int GetIncrementLocation(int amount)
    {
        int planeCount = mapManager.planes.Count;
        if (mapManager.isLoop)
        {
            return (objectLocation + amount + planeCount) % planeCount;
        }
        else
        {
            return Mathf.Min(planeCount - 1, Mathf.Max(0, objectLocation + amount));
        }
    }

    public IEnumerator Lerp(TransformData endLocation, float duration)
    {
        isTraveling = true;
        float time = 0;
        Vector3 startPos = transform.position;
        Quaternion startRotation = transform.rotation;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPos, endLocation.position, time / duration);
            transform.rotation = Quaternion.Lerp(startRotation, endLocation.rotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = endLocation.position;
        transform.rotation = endLocation.rotation;
        isTraveling = false;
    }
}

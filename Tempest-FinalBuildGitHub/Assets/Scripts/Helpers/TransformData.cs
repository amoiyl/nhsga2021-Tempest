using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TransformData
{
    public Vector3 position = Vector3.zero;
    public Quaternion rotation = Quaternion.identity;

    public TransformData() { }

    public TransformData(Transform transform)
    {
        position = transform.position;
        rotation = transform.rotation;
    }

    public void ApplyTo(Transform transform)
    {
        transform.position = position;
        transform.rotation = rotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField]
    private Material idle;

    [SerializeField]
    private Material selected;

    private Renderer planeRender;

    public bool isSpiked;

    public Plane() { }

    private void Awake()
    {
        planeRender = GetComponentInChildren<Renderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            RotateAroundEdge(30);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateAroundEdge(-30);
        }
    }

    public void SelectColor(bool isSelected = false)
    {
        planeRender.material = isSelected ? selected : idle;
    }

    public void RotateAroundEdge(float angleInDegrees)
    {
        transform.Rotate(Vector3.forward * angleInDegrees);
    }

    public Vector3 GetEulerRotation()
    {
        float deltaX = Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.PI / 180);
        float deltaY = Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.PI / 180);

        return new Vector3(deltaX, deltaY, 0);
    }

    public Vector3 GetLeftEdge()
    {
        Vector3 rotationNormalized = GetEulerRotation();
        return transform.localPosition + transform.localScale.x * rotationNormalized;
    }
}

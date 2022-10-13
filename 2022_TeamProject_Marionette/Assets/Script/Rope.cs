using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    LineRenderer lineRenderer;
    public Transform charaTransform, cubeTransform;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.005f;
        lineRenderer.endWidth = 0.005f;
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, cubeTransform.position);
        lineRenderer.SetPosition(1, charaTransform.position);
    }

}

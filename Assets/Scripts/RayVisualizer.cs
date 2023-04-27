using UnityEngine;

public class RayVisualizer : MonoBehaviour
{
    public Transform startPoint;
    public float rayLength = 100f;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        Vector3 endPoint = startPoint.position + startPoint.forward * rayLength;

        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.SetPosition(1, endPoint);
    }
}

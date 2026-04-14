using UnityEngine;

public class RayGun : MonoBehaviour
{
    public float rayDistance = 100f;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            ShootRay();
        else
            lineRenderer.enabled = false;
    }

    void ShootRay()
    {
        lineRenderer.enabled = true;

        Vector3 startPoint = transform.position;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        Vector3 endPoint = Physics.Raycast(ray, out hit, rayDistance)
            ? hit.point
            : transform.position + transform.forward * rayDistance;

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }
}

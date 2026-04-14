using UnityEngine;

public class ObjectClick : MonoBehaviour
{
    public float force = 500f;
    private Animator doorAnimator;
    private bool doorOpen = false;

    void Start()
    {
        // Le pivot s'appelle "Porte", le mesh enfant "PorteMesh"
        GameObject door = GameObject.Find("Porte");
        if (door != null)
            doorAnimator = door.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
                Manage(hit.collider.gameObject);
        }
    }

    void Manage(GameObject obj)
    {
        if (obj.name == "Cube")
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(Vector3.up * force);
        }
        else if (obj.name == "Cube1")
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(transform.forward * force);
        }
        else if (obj.name == "Porte" || obj.name == "PorteMesh")
        {
            if (doorAnimator != null)
            {
                doorOpen = !doorOpen;
                doorAnimator.Play(doorOpen ? "DoorOpen" : "DoorClose");
            }
        }
    }
}

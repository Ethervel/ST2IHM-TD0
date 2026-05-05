using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private float cooldown = 0.8f;
    private float lastHitTime = -999f;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.gameObject.CompareTag("Obstacle")) return;
        if (Time.time - lastHitTime < cooldown) return;

        lastHitTime = Time.time;
        GameManager.Instance?.AddCollision();
    }
}

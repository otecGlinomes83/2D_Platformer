using UnityEngine;

public class TargetPusher : MonoBehaviour
{
    [SerializeField] private float _pushForce = 3f;

    public void PushTarget(Rigidbody2D targetRigidbody) =>
        targetRigidbody.AddForce(GetDirection(targetRigidbody.transform.position) * _pushForce, ForceMode2D.Impulse);

    private Vector2 GetDirection(Vector3 targetPosition) =>
        (targetPosition - transform.position).normalized;
}

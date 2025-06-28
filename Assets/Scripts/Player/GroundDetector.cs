using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField,Range(-5, 5f)] private float checkGroundOffsetY = -1.8f;
    [SerializeField,Range(0, 5f)] private float checkGroundRadius = 0.3f;

    public bool IsGrounded { get; private set; }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            new Vector2(transform.position.x, transform.position.y + checkGroundOffsetY),
            checkGroundRadius);

        IsGrounded = colliders.Length > 1;
    }
}
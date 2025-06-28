using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField, Range(-5, 5f)] private float checkGroundOffsetY = -1.8f;
    [SerializeField, Range(0, 5f)] private float checkGroundRadius = 0.3f;

    public bool IsGrounded { get; private set; }
    public event System.Action<bool> OnGroundedChanged;

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            new Vector2(transform.position.x, transform.position.y + checkGroundOffsetY),
            checkGroundRadius);

        bool wasGrounded = IsGrounded;
        IsGrounded = false;

        foreach (var collider in colliders)
        {
            if (collider != null && collider.transform != transform && collider.GetComponent<Ground>() != null)
            {
                IsGrounded = true;
                break;
            }
        }

        if (wasGrounded != IsGrounded)
        {
            OnGroundedChanged?.Invoke(IsGrounded);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(
            new Vector2(transform.position.x, transform.position.y + checkGroundOffsetY),
            checkGroundRadius);
    }
}
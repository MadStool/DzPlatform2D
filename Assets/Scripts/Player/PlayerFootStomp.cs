using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerFootStomp : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 10f;
    private Rigidbody2D _playerRigidbody;

    private void Awake()
    {
        _playerRigidbody = GetComponentInParent<Rigidbody2D>();
    }

    public void BounceAfterStomp(float force)
    {
        _playerRigidbody.linearVelocity = new Vector2(_playerRigidbody.linearVelocity.x, _bounceForce);
    }
}
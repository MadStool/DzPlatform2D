using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerFootStomp : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 10f;
    private Rigidbody2D _playerRb;

    private void Awake()
    {
        _playerRb = GetComponentInParent<Rigidbody2D>();
    }

    public void BounceAfterStomp()
    {
        _playerRb.linearVelocity = new Vector2(_playerRb.linearVelocity.x, _bounceForce);
    }
}
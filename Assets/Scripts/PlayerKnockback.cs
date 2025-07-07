using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 5f;
    [SerializeField] private float _knockbackDuration = 0.3f;

    private Rigidbody2D _rb;
    private PlayerMover _mover;
    private float _knockbackTimer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        if (_knockbackTimer > 0)
        {
            _knockbackTimer -= Time.deltaTime;

            if (_knockbackTimer <= 0)
            {
                _mover.EnableMovement(true);
            }
        }
    }

    public void ApplyKnockback(bool fromRight)
    {
        float direction = fromRight ? -1f : 1f;
        _rb.linearVelocity = new Vector2(direction * _knockbackForce, _rb.linearVelocity.y);

        _knockbackTimer = _knockbackDuration;
        _mover.EnableMovement(false);
    }
}
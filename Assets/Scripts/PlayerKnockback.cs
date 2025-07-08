using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 5f;
    [SerializeField] private float _knockbackDuration = 0.3f;

    private Rigidbody2D _rigidbody;
    private PlayerMover _mover;
    private float _knockbackTimer;
    private Coroutine _knockbackCoroutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
        _rigidbody.linearVelocity = new Vector2(direction * _knockbackForce, _rigidbody.linearVelocity.y);

        _knockbackTimer = _knockbackDuration;
        _mover.EnableMovement(false);
    }

    public void StopKnockback()
    {
        _knockbackTimer = 0;
        _rigidbody.linearVelocity = Vector2.zero;
        _mover.EnableMovement(true);
    }
}
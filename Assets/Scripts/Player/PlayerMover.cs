using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [Range(0, 10)] public float speed = 1f;
    [SerializeField] private bool _movementEnabled = true;

    private Rigidbody2D _rigidbody;
    private InputHandler _inputHandler;
    private bool _facingRight = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputHandler = GetComponent<InputHandler>();
    }

    private void FixedUpdate()
    {
        if (_movementEnabled == false)
            return;

        var targetVelocity = new Vector2(_inputHandler.HorizontalInput * speed * 10f, _rigidbody.linearVelocity.y);
        _rigidbody.linearVelocity = targetVelocity;

        HandleFlip();
    }

    public void EnableMovement(bool enable)
    {
        _movementEnabled = enable;
    }

    private void HandleFlip()
    {
        if (_inputHandler.HorizontalInput < 0 && _facingRight)
        {
            Flip();
        }
        else if (_inputHandler.HorizontalInput > 0 && _facingRight == false)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _facingRight = _facingRight == false;
        transform.Rotate(0f, 180f, 0f);
    }
}
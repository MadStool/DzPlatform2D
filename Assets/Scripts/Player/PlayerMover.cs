using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [Range(0, 10)] public float speed = 1f;

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
        var targetVelocity = new Vector2(_inputHandler.HorizontalInput * speed * 10f, _rigidbody.linearVelocity.y);
        _rigidbody.linearVelocity = targetVelocity;

        HandleFlip();
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

        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
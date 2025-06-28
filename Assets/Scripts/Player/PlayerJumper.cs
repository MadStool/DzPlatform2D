using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJumper : MonoBehaviour
{
    [Range(0, 15f)] public float jumpForce = 8f;

    private Rigidbody2D _rigidbody;
    private InputHandler _inputHandler;
    private GroundDetector _groundDetector;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputHandler = GetComponent<InputHandler>();
        _groundDetector = GetComponent<GroundDetector>();
    }

    private void Update()
    {
        if (_groundDetector.IsGrounded && _inputHandler.JumpTriggered)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
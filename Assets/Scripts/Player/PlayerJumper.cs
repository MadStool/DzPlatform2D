using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJumper : MonoBehaviour
{
    [Range(0, 15f)] public float jumpForce = 8f;

    private Rigidbody2D _rigidbody;
    private InputHandler _inputHandler;
    private GroundDetector _groundDetector;
    private PlayerAnimator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputHandler = GetComponent<InputHandler>();
        _groundDetector = GetComponent<GroundDetector>();
        _animator = GetComponent<PlayerAnimator>();

        _inputHandler.OnJumpPressed += HandleJump;
        _groundDetector.OnGroundedChanged += HandleGroundedChanged;
    }

    private void OnDestroy()
    {
        if (_inputHandler != null)
            _inputHandler.OnJumpPressed -= HandleJump;

        if (_groundDetector != null)
            _groundDetector.OnGroundedChanged -= HandleGroundedChanged;
    }

    private void HandleJump()
    {
        if (_groundDetector.IsGrounded)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void HandleGroundedChanged(bool isGrounded)
    {
        _animator.SetGrounded(isGrounded);
    }
}
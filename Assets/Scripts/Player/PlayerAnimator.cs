using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private const string HorizontalMoveParam = "HorizontalMove";
    private const string IsJumpingParam = "isJumping";

    private Animator _animator;
    private InputHandler _inputHandler;
    private GroundDetector _groundDetector;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _inputHandler = GetComponent<InputHandler>();
        _groundDetector = GetComponent<GroundDetector>();
    }

    private void Update()
    {
        _animator.SetFloat(HorizontalMoveParam, Mathf.Abs(_inputHandler.HorizontalInput));
        _animator.SetBool(IsJumpingParam, _groundDetector.IsGrounded == false);
    }
}
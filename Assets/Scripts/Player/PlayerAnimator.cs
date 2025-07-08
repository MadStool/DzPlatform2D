using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("HorizontalMove");
    private static readonly int IsJumpingHash = Animator.StringToHash("isJumping");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMovementSpeed(float speed)
    {
        _animator.SetFloat(SpeedHash, Mathf.Abs(speed));
    }

    public void SetGrounded(bool isGrounded)
    {
        _animator.SetBool(IsJumpingHash, isGrounded);
    }
}
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class CharacterAnimator : MonoBehaviour
{
    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateAnimation(Vector2 moveDirection, bool isMoving)
    {
        if (_animator == null)
            return;

        _animator.SetBool(IsMovingHash, isMoving);
    }
}
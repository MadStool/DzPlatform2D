using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private const string MoveParam = "IsMoving";

    [SerializeField] private Animator _animator;
    [SerializeField] private bool _flipSprite = true;

    private SpriteRenderer _spriteRenderer;

    public Animator GetAnimator() => _animator;
    public void SetAnimator(Animator animator) => _animator = animator;

    public void Initialize(SpriteRenderer renderer)
    {
        _spriteRenderer = renderer;

        if (_animator == null && renderer != null)
            _animator = renderer.GetComponent<Animator>();
    }

    public void UpdateAnimation(Vector3 moveDirection, bool isMoving)
    {
        if (_animator != null)
        {
            _animator.SetBool(MoveParam, isMoving);
        }

        if (_flipSprite && moveDirection.x != 0 && _spriteRenderer != null)
        {
            _spriteRenderer.flipX = moveDirection.x > 0;
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private const string MoveParam = "IsMoving";

    [SerializeField] private Animator _animator;
    [SerializeField] private bool _flipSprite = true;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Assert(_spriteRenderer != null, "SpriteRenderer is missing!");
    }

    public void UpdateAnimation(Vector3 moveDirection, bool isMoving)
    {
        if (_animator == null || _spriteRenderer == null)
            return;

        _animator.SetBool(MoveParam, isMoving);

        if (_flipSprite && moveDirection.x != 0)
        {
            _spriteRenderer.flipX = moveDirection.x > 0;
        }
    }
}
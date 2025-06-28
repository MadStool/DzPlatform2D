using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 2f;

    [Header("Components")]
    [SerializeField] private PatrolPath _patrolPath;
    [SerializeField] private CharacterAnimator _characterAnimator;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Assert(_spriteRenderer != null, "SpriteRenderer is missing!");

        Debug.Assert(_patrolPath != null, "PatrolPath reference is not set!");
        Debug.Assert(_characterAnimator != null, "CharacterAnimator reference is not set!");
    }

    private void Start()
    {
        _patrolPath.Initialize(transform);
        _characterAnimator.Initialize(_spriteRenderer);

        if (_characterAnimator.GetAnimator() == null)
        {
            var animator = GetComponent<Animator>();

            if (animator != null)
            {
                _characterAnimator.SetAnimator(animator);
            }
        }
    }

    private void Update()
    {
        Vector3 moveDirection = _patrolPath.UpdatePath(transform, _moveSpeed);
        _characterAnimator.UpdateAnimation(moveDirection, _patrolPath.IsWaiting == false);
    }
}
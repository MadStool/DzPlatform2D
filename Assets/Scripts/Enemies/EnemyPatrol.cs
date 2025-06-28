using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 2f;

    [Header("Components")]
    [SerializeField] private PatrolPath _patrolPath;
    [SerializeField] private CharacterAnimator _characterAnimator;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _patrolPath.Initialize(transform);
        _characterAnimator.Initialize(_spriteRenderer);

        if (_characterAnimator.GetAnimator() == null)
            _characterAnimator.SetAnimator(GetComponent<Animator>());
    }

    private void Update()
    {
        Vector3 moveDirection = _patrolPath.UpdatePath(transform, _moveSpeed);
        _characterAnimator.UpdateAnimation(moveDirection, !_patrolPath.IsWaiting);
    }
}
using UnityEngine;

[RequireComponent(typeof(PlayerKnockback), typeof(PlayerAnimator))]
public class PlayerDamageReaction : MonoBehaviour, IDamageHandler
{
    private PlayerKnockback _knockback;
    private PlayerAnimator _animator;

    private void Awake()
    {
        _knockback = GetComponent<PlayerKnockback>();
        _animator = GetComponent<PlayerAnimator>();
    }

    public void HandleDamage(int damage, Transform damageSource)
    {
        if (damageSource != null)
        {
            bool knockFromRight = damageSource.position.x > transform.position.x;
            _knockback.ApplyKnockback(knockFromRight);
        }
    }

    public void HandleDeath()
    {
        _knockback.StopAllCoroutines();
        GetComponent<PlayerMover>().EnableMovement(false);

        Debug.Log("Player death handled");
    }
}
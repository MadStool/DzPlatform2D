using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStomp : MonoBehaviour
{
    [SerializeField] private Collider2D stompCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        PlayerFootStomp foot = collision.collider.GetComponent<PlayerFootStomp>();

        if (foot != null && collision.otherCollider == stompCollider)
        {
            KillEnemy();
            foot.BounceAfterStomp();
        }
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
    }
}
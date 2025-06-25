using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private const string AnimParamMoveSpeed = "HorizontalMove";
    private const string AnimParamIsJumping = "isJumping";
    private const string InputAxisHorizontal = "Horizontal";

    private Rigidbody2D _rigidbody;
    private float _horizontalMove = 0f;
    private bool _facingRight = true;

    [Header("Player Move Settings")]
    [Range(0, 10)] public float speed = 1f;
    [Range(0, 15f)] public float jumpForce = 8f;

    [Header("Player Anim. Settings")]
    public Animator animator;

    [Header("Ground Cheker Settings")]
    public bool isGrounded = false;
    [Range(-5, 5f)] public float checkGroundOffsetY = -1.8f;
    [Range(0, 5f)] public float checkGroundRadius = 0.3f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        _horizontalMove = Input.GetAxisRaw(InputAxisHorizontal) * speed;

        animator.SetFloat(AnimParamMoveSpeed, Mathf.Abs(_horizontalMove));

        if(isGrounded == false)
        {
            animator.SetBool(AnimParamIsJumping, true);
        }
        else
        {
            animator.SetBool(AnimParamIsJumping, false);
        }

        if(_horizontalMove < 0 && _facingRight)
        {
            Flip();
        }
        else if(_horizontalMove > 0 && _facingRight == false)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(_horizontalMove * 10f, _rigidbody.linearVelocity.y);
        _rigidbody.linearVelocity = targetVelocity;

        CheckGround();
    }

    private void Flip()
    {
        _facingRight = _facingRight == false;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            new Vector2(transform.position.x, transform.position.y + checkGroundOffsetY), checkGroundRadius);

        if(colliders.Length > 1)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
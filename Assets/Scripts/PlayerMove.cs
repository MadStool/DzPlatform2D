using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private LayerMask _groundLayer;

    [Header("References")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius = 0.2f;

    private Rigidbody2D _rb;
    private Animator _anim;
    private bool _isFacingRight = true;
    private bool _isGrounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckGround();
        HandleMovement();
        HandleJump();
        UpdateAnimations();
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 currentVelocity = _rb.linearVelocity;
        _rb.linearVelocity = new Vector2(moveInput * _moveSpeed, currentVelocity.y);

        if (moveInput > 0 && _isFacingRight == false)
            Flip();
        else if (moveInput < 0 && _isFacingRight)
            Flip();
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void UpdateAnimations()
    {
        float moveInput = Input.GetAxis("Horizontal");
        _anim.SetBool("isRunning", Mathf.Abs(moveInput) > 0.1f);
        _anim.SetBool("isGrounded", _isGrounded);
        _anim.SetFloat("verticalVelocity", _rb.linearVelocity.y);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
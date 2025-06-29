using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const KeyCode JumpKey = KeyCode.Space;
    private const KeyCode AttackKey = KeyCode.E;

    public float HorizontalInput { get; private set; }
    public event System.Action OnJumpPressed;
    public bool AttackPressed { get; private set; }

    private void Update()
    {
        HorizontalInput = Input.GetAxisRaw(HorizontalAxis);

        if (Input.GetKeyDown(JumpKey))
        {
            OnJumpPressed?.Invoke();
        }

        AttackPressed = Input.GetKeyDown(AttackKey);
    }
}
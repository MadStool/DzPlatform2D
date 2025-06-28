using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const KeyCode JumpKey = KeyCode.Space;

    public float HorizontalInput { get; private set; }
    public event System.Action OnJumpPressed;

    private void Update()
    {
        HorizontalInput = Input.GetAxisRaw(HorizontalAxis);

        if (Input.GetKeyDown(JumpKey))
        {
            OnJumpPressed?.Invoke();
        }
    }
}
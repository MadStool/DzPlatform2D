using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const KeyCode JumpKey = KeyCode.Space;

    public float HorizontalInput { get; private set; }
    public bool JumpTriggered { get; private set; }

    private void Update()
    {
        HorizontalInput = Input.GetAxisRaw(HorizontalAxis);
        JumpTriggered = Input.GetKeyDown(JumpKey);
    }
}
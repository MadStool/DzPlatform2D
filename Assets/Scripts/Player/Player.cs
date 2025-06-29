using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerJumper))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ItemCollector))]
public class Player : MonoBehaviour
{
    private void Awake()
    {
        Debug.Assert(GetComponent<InputHandler>() != null, "InputHandler is missing");
        Debug.Assert(GetComponent<GroundDetector>() != null, "GroundDetector is missing");
    }
}
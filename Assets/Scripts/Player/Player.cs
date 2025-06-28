using UnityEngine;

[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerJumper))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ItemCollector))]
public class Player : MonoBehaviour { }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PlayerMovement Movement { get; private set; }
    public PlayerJump Jump { get; private set; }

    public Rigidbody2D rb { get; private set; }

    public bool IsGrounded { get; private set; } = false;

    [Header("Grounded Check Variables")]
    [SerializeField] private Transform feet;
    [SerializeField] private float groundCheckRayLength;
    [SerializeField] private LayerMask groundLayerMask;

    private void Awake()
    {
        Movement = GetComponent<PlayerMovement>();
        Jump = GetComponent<PlayerJump>();

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        IsGrounded = Physics2D.Raycast(feet.position, Vector2.down, groundCheckRayLength, groundLayerMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(feet.position, Vector2.down * groundCheckRayLength);
    }

}

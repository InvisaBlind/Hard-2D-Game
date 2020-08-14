using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    private Player player;

    [Header("Jump Values")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float timeToJumpHeight;

    [Header("Gravity Multipliers")]
    [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private float lowJumpGravityMultiplier;

    private float gravity;
    private float jumpForce;
    
    [ExecuteInEditMode]
    private void OnValidate()
    {
        fallGravityMultiplier = Mathf.Max(1, fallGravityMultiplier);
        lowJumpGravityMultiplier = Mathf.Max(1, lowJumpGravityMultiplier);
        gravity = 2 * jumpHeight / Mathf.Pow(timeToJumpHeight, 2);
        jumpForce = Mathf.Abs(gravity) * timeToJumpHeight;
        Physics2D.gravity = Vector2.down * gravity;
    }

    private void Awake()
    {
        player = GetComponent<Player>();     
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && player.IsGrounded)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, jumpForce);
        }

        if (player.rb.velocity.y < 0)
        {
            player.rb.velocity += Vector2.up * Physics2D.gravity * (fallGravityMultiplier - 1) * Time.deltaTime;
        }
        else if (player.rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            player.rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpGravityMultiplier - 1) * Time.deltaTime; 
        }
    }

}

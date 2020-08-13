using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float stopForce;

    private float speed = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(xInput) > 0f)
        {
            float targetSpeed = xInput * maxSpeed;
            speed = Mathf.MoveTowards(speed, targetSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            speed = Mathf.MoveTowards(speed, 0f, stopForce * Time.deltaTime);
        }
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}

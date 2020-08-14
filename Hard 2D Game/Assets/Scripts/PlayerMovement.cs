using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    #region Variables

    private Rigidbody2D rb;

    [SerializeField] private float maxSpeed = 0f;
    [SerializeField] private float timeToMaxSpeed = 0f;
    [SerializeField] private float timeToStop = 0f;
    [SerializeField] private float reverseSpeedMultiplier = 1f;

    private float acceleration
    {
        get
        {
            return maxSpeed / timeToMaxSpeed;
        }
    }

    private float stopForce
    {
        get
        {
            return Mathf.Abs(speed) / timeToStop;
        }
    }

    private float xInput;
    private float speed = 0f;

    #endregion

    #region Unity Functions

    [ExecuteInEditMode]
    private void OnValidate()
    {
        timeToMaxSpeed = Mathf.Max(timeToMaxSpeed, 0);
        timeToStop = Mathf.Max(timeToStop, 0);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        Move();
    }

    #endregion

    #region Helper Functions

    private void Move()
    {
        if (Mathf.Abs(xInput) > 0f)
        {
            if (timeToMaxSpeed == 0)
            {
                speed = xInput * maxSpeed;
            }
            else
            {
                float targetSpeed = xInput * maxSpeed;
                float tempAcceleration = acceleration;
                if (Mathf.Sign(xInput) != Mathf.Sign(rb.velocity.x))
                {
                    tempAcceleration *= reverseSpeedMultiplier;
                }
                speed = Mathf.MoveTowards(speed, targetSpeed, tempAcceleration * Time.deltaTime);
            }
        }
        else
        {
            if (timeToStop == 0)
            {
                speed = 0;
            }
            else
            {
                speed = Mathf.MoveTowards(speed, 0f, stopForce * Time.deltaTime);
            }
        }
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    #endregion

}

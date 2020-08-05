using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpVelocity = 5f;
    public GameObject eyes;
    bool canJump;
    public bool jumpEnabled;
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    private void Start()
    {
        
        
    }
    void Awake()
    {
        rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 movement = new UnityEngine.Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0f, 0f);
        transform.position += movement;
        jump();
        faceCorrectDirection();
    }
    void jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && canJump && jumpEnabled)
        {
            rigidbody.AddForce(UnityEngine.Vector2.up * jumpVelocity, ForceMode2D.Force);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump = false;
        }
    }
    void faceCorrectDirection()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            eyes.transform.position = UnityEngine.Vector3.Lerp(eyes.transform.position, new UnityEngine.Vector3(transform.position.x - 0.02056f, transform.position.y, transform.position.z), .01f);
        } else if (Input.GetAxis("Horizontal") < 0)
        {
            eyes.transform.position = UnityEngine.Vector3.Lerp(eyes.transform.position, new UnityEngine.Vector3(transform.position.x - 0.15f, transform.position.y, transform.position.z), .01f);
        }
    }
}

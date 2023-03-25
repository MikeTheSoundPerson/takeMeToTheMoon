using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Movement : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter walk;
    [SerializeField] private StudioEventEmitter jump;
    public float speed = 10f;
    public Vector2 jumpHeight;
    private float time = 0f;
    private Rigidbody2D rb;
    private bool grounded = true;
    private enum MovementState { Idle, Walking, Climbing, Jumping, Falling};

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        MovementState movementState = MovementState.Idle;


        var horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        //change forward direction
        if (horizontalInput > 0)
        {
            movementState = MovementState.Walking;
            transform.rotation = Quaternion.identity;

        }
        else if (horizontalInput < 0)
        {
            movementState = MovementState.Walking;
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else
        {
            movementState = MovementState.Idle;
        }

        //jump
        if (Input.GetAxis("Jump") > 0 && grounded)
        {
            grounded = false;
            movementState = MovementState.Jumping;
            GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);
            jump?.Play();
        }


        if (movementState == MovementState.Walking && Time.time >= time + 0.2f)
        {
            if(walk != null)
                walk.Play();
            time = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}

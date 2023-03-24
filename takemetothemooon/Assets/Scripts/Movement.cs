using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        //change forward direction
        if (horizontalInput > 0)
        {
            transform.rotation = Quaternion.identity;
        }
        else if (horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
    }
}

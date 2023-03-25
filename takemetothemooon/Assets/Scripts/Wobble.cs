using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    public Vector2 offset;
    public Vector2 startPosition;

    public float wobbelSpeed = 5f;
    public int index = 0;

    public Rigidbody2D rb;

    Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        position = Vector2.Lerp(transform.position, startPosition + offset * Mathf.Sin(Time.time * wobbelSpeed + index%3), wobbelSpeed);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }
}

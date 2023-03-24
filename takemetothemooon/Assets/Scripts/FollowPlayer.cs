using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float offset = 3f;
    public float wobbelSpeed = 5f;
    public float moveSpeed = 0.1f;
    private Vector2 position;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        position = Vector2.Lerp(transform.position, player.position + player.right * offset + player.up * Mathf.Sin(Time.time * wobbelSpeed), moveSpeed);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }
}

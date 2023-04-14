using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedMovement : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider2D;
    [SerializeField] private LayerMask groundLayer;
    [Range(0, 10f)] [SerializeField] private float speed = 5f;

    float horizontal = 0f;
    float lastJumpY = 0f;


    private bool isFacingRight = true;
    bool jump = false;
    bool jumpheld = false;


    bool crouchHeld = false;
    bool isUnderPlatform = false;
    bool isCloseToLadder = false;
    bool climbHeld = false;
    bool hasStartedClimb = false;

    private Transform ladder;
    private float vertical = 0f;
    [SerializeField] private float climbSpeed = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * speed;
        vertical = Input.GetAxisRaw("Climb") * climbSpeed;


        if(IsOnGround() && horizontal.Equals(0) && !hasStartedClimb)
            GetComponentInChildren<Animator>()?.Play("CharacterIdle");
        else if(IsOnGround() && !hasStartedClimb && (horizontal > 0 || horizontal < 0))
            GetComponentInChildren<Animator>()?.Play("CharacterWalk");

        climbHeld = (isCloseToLadder && Input.GetButton("Climb")) ? true : false;

        if (climbHeld)
        {
            if (!hasStartedClimb)
            {
                hasStartedClimb = true;
            }
            else
            {
                if (hasStartedClimb)
                {
                    GetComponentInChildren<Animator>()?.Play("CharacterClimb");
                }
            }
        }
        
        
        if (IsOnGround() && Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (!IsOnGround() && !hasStartedClimb)
        {
            if (lastJumpY < transform.position.y)
            {
                lastJumpY = transform.position.y;
                GetComponentInChildren<Animator>().Play("CharacterJump");
            }
            else if(lastJumpY > transform.position.y)
            {
                GetComponentInChildren<Animator>().Play("CharacterFall");
            }
            

        }
    }

    void FixedUpdate()
    {
        float moveFactor = horizontal * Time.fixedDeltaTime;

        rb.velocity = new Vector2(moveFactor * 100f, rb.velocity.y);

        if (moveFactor > 0 && !isFacingRight) FlipSprite();
        else if(moveFactor < 0 && isFacingRight) FlipSprite();

        if (jump)
        {
            float jumpvel = 20f;
            rb.velocity = Vector2.up * jumpvel;
            jump = false;
        }

        if(hasStartedClimb && !climbHeld)
        {
            if(horizontal > 0 || horizontal < 0) ResetClimbing();
        }
        else if(hasStartedClimb && climbHeld)
        {
            float height = GetComponentInChildren<SpriteRenderer>().size.y;
            Debug.Log("height: " + height);
            float topHandlerY = Half(ladder.transform.GetChild(0).transform.position.y + height);
            Debug.Log("topHandlerY: " + topHandlerY);
            float bottomHandlerY = Half(ladder.transform.GetChild(1).transform.position.y + height);
            Debug.Log("bottomHandlerY: " + bottomHandlerY);
            float transformY = Half(transform.position.y);
            Debug.Log("transformY: " + transformY);
            float transformVY = transformY + vertical;
            Debug.Log("transformVY: " + transformVY);

            if (transformVY > topHandlerY || transformVY < bottomHandlerY)
            {
                ResetClimbing();
            }
            else if (transformY <= topHandlerY && transformY >= bottomHandlerY)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                if (!transform.position.x.Equals(ladder.transform.position.x))
                {
                    transform.position = new Vector3(ladder.transform.position.x, transform.position.y, transform.position.z);
                }
                GetComponentInChildren<Animator>()?.Play("CharacterClimb");
                Vector3 forwardDirection = new Vector3(0, -1, 0);
                Debug.Log("forwardDirection: " + forwardDirection);
                Vector3 newPos = Vector3.zero;
                if(vertical > 0)
                {
                    newPos = transform.position + forwardDirection * Time.deltaTime * climbSpeed;
                    Debug.Log("newPos: " + newPos + forwardDirection * Time.deltaTime * climbSpeed);
                }
                else if(vertical < 0)
                {
                    newPos = transform.position - forwardDirection * Time.deltaTime * climbSpeed;
                    Debug.Log("newPos: " + newPos + forwardDirection * Time.deltaTime * climbSpeed);
                }
                if (newPos != Vector3.zero) 
                {
                    rb.velocity = Vector2.zero;
                    rb.MovePosition(newPos);
                }
            }
        }
    }

    private void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        
        Vector3 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;
    }

    private bool IsOnGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(circleCollider2D.bounds.center, circleCollider2D.radius, Vector2.down, 0.1f, groundLayer);
        if (raycastHit2D && !lastJumpY.Equals(-1000)) lastJumpY = -1000f;
        return raycastHit2D.collider != null;
    }

    private void OnTriggerStay2D(Collider2D collison)
    {
        if (collison.gameObject.tag.Equals("Ladder"))
        {
            isCloseToLadder = true;
            ladder = collison.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collison)
    {
        if (collison.gameObject.tag.Equals("Ladder"))
        {
            isCloseToLadder = false;
            ladder = null;
        }
    }

    public static float Half(float value)
    {
        return Mathf.Floor(value) + 0.5f;
    }

    private void ResetClimbing()
    {
        Debug.Log("ResetClimbing");
        if(hasStartedClimb)
        {
            hasStartedClimb = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            isCloseToLadder = false;
            //transform.position = new Vector3(transform.position.x, Half(transform.position.y),transform.position.z);
        }
    }
}

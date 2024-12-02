using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private float lastInputX = 0f;
    private float lastInputY = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Prevent diagonal movement: only use one axis at a time
        if (Mathf.Abs(moveX) > Mathf.Abs(moveY))  // Prioritize horizontal movement
        {
            movement.x = moveX;
            movement.y = 0;
            lastInputX = moveX;
        }
        else if (Mathf.Abs(moveY) > Mathf.Abs(moveX))  // Prioritize vertical movement
        {
            movement.y = moveY;
            movement.x = 0;
            lastInputY = moveY;
        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }

        animator.SetFloat("InputX", movement.x);
        animator.SetFloat("InputY", movement.y);

        if (movement.sqrMagnitude > 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);

            // Only set LastInputX if the player was moving horizontally
            if (lastInputX != 0)
            {
                animator.SetFloat("LastInputX", lastInputX);
                animator.SetFloat("LastInputY", 0); // Disregard Y input
            }
            // Only set LastInputY if the player was moving vertically
            else if (lastInputY != 0)
            {
                animator.SetFloat("LastInputX", 0); // Disregard X input
                animator.SetFloat("LastInputY", lastInputY);
            }
        }
    }

    void FixedUpdate()
    {
        if (movement.sqrMagnitude > 0)
        {
            rb.velocity = movement * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}

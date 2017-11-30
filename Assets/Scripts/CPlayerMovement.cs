using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerMovement : MonoBehaviour
{
    #region Public Attributes
    [Tooltip("Adjusts the player movement speed.")]
    public float moveSpeed;
    [Tooltip("Adjusts the player jumping force.")]
    public float jumpForce;
    [Tooltip("Radius of a circle to detect if the player is grounded.")]
    public float groundDetectionDistance;
    [Tooltip("Point from where the circle of ground detection is located.")]
    public Transform groundPoint;
    [Tooltip("Layer marked as ground.")]
    public LayerMask groundLayerMask;
    #endregion

    #region Private Attributes
    private Animator animator;
    private Rigidbody2D rigidBody;
    private float hInput;
    private bool isTouchingGround;
    private Vector3 rightWalkingScale;
    private Vector3 leftWalkingScale;
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        hInput = 0f;
        rightWalkingScale = transform.localScale;
        leftWalkingScale = new Vector3(-rightWalkingScale.x, rightWalkingScale.y, rightWalkingScale.z);
    }

    private void Update()
    {
        Move();
        DetectPlayerTouchingGround();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    /// <summary>
    /// Checks if the player is pressing any key that is tagged as
    /// horizontal input, and then applies the appropiate horizontal
    /// movement.
    /// </summary>
    private void Move()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        if (isTouchingGround) animator.SetBool("Walking", hInput != 0f);
        else animator.SetBool("Walking", false);
        if (hInput < 0f)
            transform.localScale = leftWalkingScale;
        else if(hInput > 0f)
            transform.localScale = rightWalkingScale;
        transform.Translate(Vector3.right * hInput * Time.deltaTime * moveSpeed);
    }

    private void Jump()
    {
        if(isTouchingGround && Input.GetAxis("Jump") > 0f)
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Detects if the player is touching anything in the ground Layer by
    /// casting a circle and checking if a collider is returned.
    /// </summary>
    private void DetectPlayerTouchingGround()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundPoint.position, groundDetectionDistance,
            groundLayerMask);
        animator.SetBool("Jumping", !isTouchingGround);
    }
}

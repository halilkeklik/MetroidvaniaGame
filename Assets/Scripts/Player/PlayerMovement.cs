using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum MovementStates
    {
        Idle,
        Running,
        Jumping,
        Attacking
    }

    public enum FacingDirection
    {
        Right,
        Left
    }

    [Header("Movement values")]
    public float movementSpeed;
    public float jumpForce;

    [Header("Raycast lenght and layermask")]
    public float isGroundedRayLength;
    public LayerMask platformLayerMask;

    [Header("Movement States")]
    public MovementStates movementStates;
    public FacingDirection facingDirection;

    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    private PlayerAnimationController animController;
    private float dirX;

    private void Awake()
    {
        rigidBody2D = GetComponent < Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animController = GetComponent<PlayerAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        HandleJump();
    }



    private void FixedUpdate()
    {
        HandleMovement();
        PlayAnmationsBasedOnState();
        SetCharacterDirection();
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {

            rigidBody2D.velocity = Vector2.up * jumpForce;
        }
    }
    private void HandleMovement()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rigidBody2D.velocity = new Vector2(dirX * movementSpeed, rigidBody2D.velocity.y);
    }


    public bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(spriteRenderer.bounds.center,
            spriteRenderer.bounds.size, 0f, Vector2.down,
            isGroundedRayLength, platformLayerMask);

        return raycastHit2D.collider != null;
    }

    private void SetCharacterDirection()
    {
        switch (facingDirection)
        {
            case FacingDirection.Right:
                spriteRenderer.flipX = false;
                break;
            case FacingDirection.Left:
                spriteRenderer.flipX = true;
                break;
        }
    }

    private void PlayAnmationsBasedOnState()
    {
        switch (movementStates)
        {
            case MovementStates.Idle:
                animController.PlayIdleAnim();
                break;
            case MovementStates.Running:
                animController.PlayRunningAnim();
                break;
            case MovementStates.Jumping:
                animController.PlayJumpingAnim();
                break;
            default:
                break;
        }
    }
}

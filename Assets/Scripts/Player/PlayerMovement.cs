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
        Falling,
        Attacking
    }

    public enum FacingDirection
    {
        Right,
        Left
    }

    [Header("Movement values")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    [Header("Raycast lenght and layermask")]
    [SerializeField] private float isGroundedRayLength;
    [SerializeField] private LayerMask platformLayerMask;

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
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case FacingDirection.Left:
                transform.localScale = new Vector3(-1, 1, 1);
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
            case MovementStates.Falling:
                animController.PlayFallingAnim();
                break;
            case MovementStates.Attacking:
                animController.TiggerAttackAnim();
                break;
            default:
                break;
        }
    }
}

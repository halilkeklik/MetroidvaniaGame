using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private PlayerCombat combat;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        combat = GetComponent<PlayerCombat>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1") && playerMovement.movementStates != PlayerMovement.MovementStates.Jumping)
        {
            StartCoroutine(AttackOrder());
        }
        SetPlayerState();
    }

    private  void SetPlayerState()
    {
        if (playerMovement.isGrounded())
        {
            if(combat.isAttacking)
                return;
            switch (rb.velocity.x)
            {
                case 0:
                    playerMovement.movementStates = PlayerMovement.MovementStates.Idle;
                    break;
                case > .1f:
                    playerMovement.facingDirection = PlayerMovement.FacingDirection.Right;
                    playerMovement.movementStates = PlayerMovement.MovementStates.Running;
                    break;
                case < .1f:
                    playerMovement.facingDirection = PlayerMovement.FacingDirection.Left;
                    playerMovement.movementStates = PlayerMovement.MovementStates.Running;
                    break;
            }
        }
        else switch (rb.velocity)
        {
            case { y: > .1f, x: > .1f }:
                playerMovement.facingDirection = PlayerMovement.FacingDirection.Right;
                playerMovement.movementStates = PlayerMovement.MovementStates.Jumping;
                break;
            case { y: > .1f, x: < .1f }:
                playerMovement.facingDirection = PlayerMovement.FacingDirection.Left;
                playerMovement.movementStates = PlayerMovement.MovementStates.Jumping;
                break;
            case { y: < .1f, x: > .1f }:
                playerMovement.facingDirection = PlayerMovement.FacingDirection.Right;
                playerMovement.movementStates = PlayerMovement.MovementStates.Falling;
                break;
            case { y: < .1f, x: < .1f }:
                playerMovement.facingDirection = PlayerMovement.FacingDirection.Left;
                playerMovement.movementStates = PlayerMovement.MovementStates.Falling;
                break;
        }
    }

    private IEnumerator AttackOrder()
    {
        if(combat.isAttacking)
            yield break;

        combat.isAttacking = true;
        playerMovement.movementStates = PlayerMovement.MovementStates.Attacking;
        

        yield return new WaitForSeconds(0.01f);
        
        combat.Attack();

        combat.isAttacking = false;
    }
}

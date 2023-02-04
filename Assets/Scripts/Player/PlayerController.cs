using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAnimationController animationController;
    private PlayerCombat combat;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    [SerializeField] private float speed;
    [SerializeField] private float jumpforce;
    [SerializeField] private LayerMask jumpableGround;
    private float dirX;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animationController = GetComponent<PlayerAnimationController>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        combat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(AttackOrder());
        }
        AnimationState();
    }

    private  void AnimationState()
    {
        if (dirX > 0f)
        {
            animationController.PlayRunningAnim();
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            animationController.PlayRunningAnim();
            sprite.flipX = true;
        }
        else
        {
            animationController.PlayIdleAnim();
        }

        if (rb.velocity.y > .1f)
        {
            animationController.PlayJumpingAnim();
        }
        else if(rb.velocity.y < -.1f)
        {
            animationController.PlayFallingAnim();
        }
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private IEnumerator AttackOrder()
    {
        if(combat.isAttacking)
            yield break;

        combat.isAttacking = true;
        animationController.TiggerAttackAnim();

        yield return new WaitForSeconds(0.1f);
        
        combat.Attack();

        combat.isAttacking = false;
    }
}

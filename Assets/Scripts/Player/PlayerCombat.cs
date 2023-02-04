using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float damage;
    [SerializeField] private float attackRangeX;
    [SerializeField] private float attackRangeY;
    [SerializeField] private Transform attackPos;
    [SerializeField] private LayerMask whatIsEnemy;
    private float nextAttackTime = 0f;
    [SerializeField] private float attackRate;
    
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    void Attack()
    {
        anim.SetTrigger("attack");
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position,
            new Vector2(attackRangeX, attackRangeY),0,whatIsEnemy);
        foreach (var t in enemiesToDamage)
        {
            t.GetComponent<Health>().TakeDamage(damage);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}

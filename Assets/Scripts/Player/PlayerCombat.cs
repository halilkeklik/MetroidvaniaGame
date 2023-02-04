using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float attackRangeX;
    [SerializeField] private float attackRangeY;
    [SerializeField] private Transform attackPos;
    [SerializeField] private LayerMask whatIsEnemy;
    public bool isAttacking = false;
    public void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position,
            new Vector2(attackRangeX, attackRangeY),0,whatIsEnemy);
        if(enemiesToDamage==null)
            return;
        
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

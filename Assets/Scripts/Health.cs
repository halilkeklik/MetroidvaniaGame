using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

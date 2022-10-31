using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MeleeEnemy : Fighter
{

    public int damage = 1;
    public float force = 2.0f; // knockback force

    protected override void Death()
    {
        Destroy(gameObject);
    }

    protected override void OnCollide(Collider2D coll)
    {
        // Deal damage only if target has player tag
        if (coll.tag == "Player")
        {
            Damage dmg = new Damage
            {
                damageAmmount = damage,
                knockBack = force,
                origin = transform.position
            };
            rb.velocity = Vector2.zero; // after hit slow down to 0
            
        }
    }
}

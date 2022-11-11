using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballExplosion : Explosion
{
    // animation clip of explosion

    protected override void OnCollide(Collider2D coll)
    {
        // send damage to enemy if hit by explosion
        if (coll.CompareTag("Enemy"))
        {
            Damage dmg = new Damage()
            {
                damageAmmount = explosionDamage,
                knockBack = knockback,
                origin = transform.position
            };
            
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}

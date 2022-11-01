using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MeleeEnemy : Collidable
{

    public int damage = 1;
    public float force = 2.0f; // knockback force

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
            coll.SendMessage("ReceiveDamage", dmg);
            
        }
    }
}

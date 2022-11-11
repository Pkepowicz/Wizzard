using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostExplosion : Explosion
{
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
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

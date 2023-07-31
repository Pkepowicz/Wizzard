using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffHit : Explosion
{
    protected override void OnCollide(Collider2D coll)
    {
        // send damage to player if hit by explosion
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

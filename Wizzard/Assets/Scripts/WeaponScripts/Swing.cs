using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : Explosion
{
    protected override void OnCollide(Collider2D coll)
    {
        // send damage to player if hit by explosion
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

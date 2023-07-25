using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    private float knockback;

    public void PassParameters(float dmg, float knb)
    {
        damage = dmg;
        knockback = knb;
    }

    protected override void OnProjectilePlayerHit(Collider2D coll)
    {
        Damage dmg = new Damage()
        {
            damageAmmount = damage,
            knockBack = knockback,
            origin = transform.position
        };

        coll.SendMessage("ReceiveDamage", dmg);
        Destroy(gameObject);
    }

    protected override void OnProjectileEnemyHit(Collider2D coll)
    {
        
    }
}

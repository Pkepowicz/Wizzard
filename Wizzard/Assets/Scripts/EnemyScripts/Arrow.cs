using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    private float damage;
    
    public void PassParameters(float dmg)
    {
        damage = dmg;
    }

    protected override void OnProjectilePlayerHit(Collider2D coll)
    {
        coll.SendMessage("ReceiveDamage", damage);
        Destroy(gameObject);
    }

    protected override void OnProjectileEnemyHit(Collider2D coll)
    {
        
    }
}

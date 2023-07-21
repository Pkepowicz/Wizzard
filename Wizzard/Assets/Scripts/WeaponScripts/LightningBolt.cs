using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : Projectile
{
    public GameObject chainLightningEffect;
    public GameObject beenStruck;
    
    protected override void OnProjectileEnemyHit(Collider2D coll)
    {
        base.OnProjectileEnemyHit(coll);
        Instantiate(chainLightningEffect, coll.transform.position, Quaternion.identity, coll.gameObject.transform);
        Instantiate(beenStruck, coll.transform.position, Quaternion.identity, coll.gameObject.transform);
    }
}

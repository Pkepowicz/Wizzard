using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    // possible modifications to basic fireball
    private bool explodeAtDeath = false;
    private bool pullEnemies = false;
    
    // variables for pulling enemies
    public float pullRadius = 3f;
    public float pullForce = 1f;

    public void PassParameters(bool explode, bool pull)
    {
        explodeAtDeath = explode;
        pullEnemies = pull;
        Debug.Log(explodeAtDeath);
    }

    private void Update()
    {
        base.Update();
        if (pullEnemies is true)
        {
            
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        base.OnCollide(coll);
        
    }

    protected override void OnProjectileFinish()
    {
        if (explodeAtDeath is true)
        {
            Debug.Log("Explode");
        }
        base.OnProjectileFinish();
    }

    protected override void OnProjectileWallHit()
    {
        if (explodeAtDeath is true)
        {
            Debug.Log("Explode");
        }
        base.OnProjectileFinish();
    }
}

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
    
    // variables for explosion
    public int explosionDamage = 3;
    public float explosionRadius = 1f; // basic radius gets multiplied by this value 
    public float explosionKnockbackForce = 2f;

    
    // prefab to instanciate when projectile explodes
    public GameObject explosionPrefab;
    

    // when creating projectile, pass parameters abut it
    public void PassParameters(bool explode, bool pull)
    {
        explodeAtDeath = explode;
        pullEnemies = pull;
    }

    protected override void Update()
    {
        base.Update();
        if (pullEnemies is true)
        {
            //TODO: add pulling enemies while traveling
        }
    }

    // what to do with projectile when it's life ends
    protected override void OnProjectileFinish()
    {
        if (explodeAtDeath is true)
        {
            Explode();
        }
        Destroy(gameObject);
    }

    
    // what to do with projectile when it hits the wall
    protected override void OnProjectileWallHit()
    {
        if (explodeAtDeath is true)
        {
            Explode();
        }
        Destroy(gameObject);
    }

    // create explosion game object and pass parameters
    private void Explode()
    {
        Debug.Log("Exploding");
        GameObject currentExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
        // pass parameters about explosion to newly created explosion object
        currentExplosion.GetComponent<FireballExplosion>().PassParameters(explosionDamage, explosionRadius, explosionKnockbackForce);
    }
}

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
    public float explosionRadius = 1f; // 0.3 gets multiplayed by radius, 1 is default value
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

    protected override void OnProjectileFinish()
    {
        if (explodeAtDeath is true)
        {
            Explode();
        }
        Destroy(gameObject);
    }

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
        FireballExplosion explosionScript = currentExplosion.GetComponent<FireballExplosion>();
        
        explosionScript.PassParameters(explosionDamage, explosionRadius, explosionKnockbackForce);
        
        Debug.Log("Destroying fireball");
    }
}

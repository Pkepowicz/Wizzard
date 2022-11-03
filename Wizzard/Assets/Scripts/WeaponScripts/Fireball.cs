using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    // variables for damaging enemies
    private int damage;
    public float knockbackForce;
    
    
    // possible modifications to basic fireball
    private bool explodeAtDeath = false;
    private bool igniteEnemies = false;

    // variables for burning
    public GameObject burn;
    public float igniteEfficieny = 0.5f; // what % of impact damage will be dealt as ignite damage during its duration
    
    // variables for explosion
    [Header("Explosion settings")]
    public int explosionDamage = 3;
    public float explosionRadius = 1f; // basic radius gets multiplied by this value 
    public float explosionKnockbackForce = 2f;
    
    // prefab to instanciate when projectile explodes
    public GameObject explosionPrefab;
    

    // when creating projectile, pass parameters abut it
    public void PassParameters(int dmg ,bool explode, bool ignite)
    {
        damage = dmg;
        explodeAtDeath = explode;
        igniteEnemies = ignite;
    }
    
    // what to do with projectile when it hits an enemy
    protected override void OnProjectileEnemyHit(Collider2D coll)
    {
        Damage dmg = new Damage()
        {
            damageAmmount = damage,
            knockBack = knockbackForce,
            origin = transform.position
        };

        if (igniteEnemies)
        {
            GameObject enemyHit = coll.gameObject;
            GameObject currentBurn =
                Instantiate(burn, enemyHit.transform.position, Quaternion.identity, enemyHit.transform);
            // pass paremeters to burn added to enemy
            currentBurn.GetComponent<Burn>().CalculateBurnDamage(damage, igniteEfficieny);
        }
        
        coll.SendMessage("ReceiveDamage", dmg);
        if (explodeAtDeath is true)
        {
            Explode();
        }
        Destroy(gameObject);
    }
    
    // what to do with projectile when it's lifetime ends
    protected override void OnProjectileEnd()
    {
        if (explodeAtDeath is true)
        {
            Explode();
        }
        Destroy(gameObject);
    }

    
    // what to do with projectile when it hits the wall
    protected override void OnProjectileWallHit(Collider2D coll)
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
        //Debug.Log("Exploding");
        GameObject currentExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
        // pass parameters about explosion to newly created explosion object
        currentExplosion.GetComponent<FireballExplosion>().PassParameters(explosionDamage, explosionRadius, explosionKnockbackForce);
    }
}

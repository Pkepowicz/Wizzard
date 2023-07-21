using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : Collidable
{
    // variables for damaging enemies
    public float damage;
    public float knockbackForce;

    public float speed = 2.5f;
    public float lifetime = 5f; // Not sure if needed
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }
    

    protected virtual void FixedUpdate()
    {
        // Go forward from launch offset untill you exceed life time
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (Time.time - startTime > lifetime)
            OnProjectileEnd();
    }

    protected override void OnCollide(Collider2D coll)
    {
        // interact with enemy, and destroy itself
        if (coll.CompareTag("Enemy"))
        {
            OnProjectileEnemyHit(coll);
        }
        
        else if (coll.CompareTag("Structure"))
        {
            OnProjectileWallHit(coll);
        }
        
        else if (coll.CompareTag("Player"))
        {
            OnProjectilePlayerHit(coll);
        }
    }

    // what to do with projectile when it hits an, e.g explode
    protected virtual void OnProjectileEnemyHit(Collider2D coll)
    {
        Damage dmg = new Damage()
        {
            damageAmmount = damage,
            knockBack = knockbackForce,
            origin = transform.position
        };
        
        coll.SendMessage("ReceiveDamage", dmg);
        Destroy(gameObject);
    }

    // what to do with projectile when it expires, e.g explode
    protected virtual void OnProjectileEnd()
    {
        Destroy(gameObject);
    }

    // some projectiles may bounce of the walls
    protected virtual void OnProjectileWallHit(Collider2D coll)
    {
        Destroy(gameObject);
    }

    protected virtual void OnProjectilePlayerHit(Collider2D coll)
    {
        Destroy(gameObject);
    }
    
}

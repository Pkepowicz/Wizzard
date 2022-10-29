using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Collidable
{
    public float speed = 20.0f;
    public float lifetime = 5f; // Not sure if needed
    public int damage = 1;
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
            OnProjectileFinish();
    }

    protected override void OnCollide(Collider2D coll)
    {
        // interact with enemy, and destroy itself
        if (coll.CompareTag("Enemy"))
        {
            OnProjectileFinish();
        }
        
        else if (coll.CompareTag("Structure"))
        {
            OnProjectileWallHit();
        }
    }

    // what to do with projectile when it hits enemy or expires, e.g explode
    protected virtual void OnProjectileFinish()
    {
        Destroy(gameObject);
    }

    // some projectiles may bounce of the walls
    protected virtual void OnProjectileWallHit()
    {
        Destroy(gameObject);
    }
    
}

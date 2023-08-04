using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Explosion : Collidable
{
    // animation clip of explosion
    public AnimationClip animClip;
    
    public float explosionDamage;
    public float knockback;

   
    
    protected virtual void Start()
    {
        // destroy this gameObject at the end of its animation
        Destroy(gameObject, animClip.length);

        
    }
    
    public void PassParameters(float damage, float radius, float knockbackForce)
    {
        explosionDamage = damage;
        knockback = knockbackForce;
        gameObject.transform.localScale = new Vector3(1.5f * radius, 1.5f * radius, 1);
    }
}

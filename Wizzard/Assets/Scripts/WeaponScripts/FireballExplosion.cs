using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballExplosion : Collidable
{
    // animation clip of explosion
    public AnimationClip animClip;
    
    private int explosionDamage;
    private float knockback;
    
    void Start()
    {
        // destroy this gameObject at the end of its animation
        Destroy(gameObject, animClip.length);
    }
    

    // change parameters to one passed from fireball, so all variables are adjustable in fireball
    public void PassParameters(int damage, float rad, float knockbackForce)
    {
        explosionDamage = damage;
        knockback = knockbackForce;
        gameObject.transform.localScale = new Vector3(1.5f * rad, 1.5f * rad, 1);
    }

    protected override void OnCollide(Collider2D coll)
    {
        // send damage to enemy if hit by explosion
        if (coll.CompareTag("Enemy"))
        {
            Damage dmg = new Damage()
            {
                damageAmmount = explosionDamage,
                knockBack = knockback,
                origin = transform.position
            };
            
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}

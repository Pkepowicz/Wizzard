using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballExplosion : Collidable
{
    public AnimationClip animClip;
    
    public bool explodeNow = false;

    //private CircleCollider2D explosionCollider;

    private int explosionDamage;
    private float knockback;
    void Start()
    {
        //explosionCollider = GetComponent<CircleCollider2D>();
        //Debug.Log(explosionCollider.radius);
        // destroy this gameObject at the end of its animation
        Destroy(gameObject, animClip.length);
        //Debug.Log(animClip.length);
        
        
    }

    
    /*void Update()
    {
        if (explodeNow)
        {
            base.Update();
            explodeNow = false;
            Debug.Log("Dealt Damage");
        }
        
    }*/

    public void PassParameters(int damage, float rad, float knockbackForce)
    {
        explosionDamage = damage;
        knockback = knockbackForce;
        //Debug.Log("Passed parameters");
         
         gameObject.transform.localScale = new Vector3(1.5f * rad, 1.5f * rad, 1);
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            Damage dmg = new Damage()
            {
                damageAmmount = explosionDamage,
                knockBack = knockback,
                origin = transform.position
            };
            
            coll.SendMessage("ReceiveDamage", dmg);
            Debug.Log("Sending Damage");
        }
    }
}

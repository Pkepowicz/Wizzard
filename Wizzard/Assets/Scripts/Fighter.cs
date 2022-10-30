using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Collidable
{
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    public float immunityTime = 0;
    private float lastImmune;

    public Rigidbody2D rb;

    protected virtual void ReceiveDamage(Damage dmg) // knockback function
    {
        if (Time.time - lastImmune > immunityTime)
        {
            hitPoint -= dmg.damageAmmount;
            rb.AddForce((transform.position - dmg.origin).normalized * dmg.knockBack, ForceMode2D.Impulse);
            lastImmune = Time.time;
            if (hitPoint <= 0)
            {
                hitPoint = 0;
                Death();
            }
        }
    }

    protected virtual void ReceiveDamage(int dmg)
    {
        if (Time.time - lastImmune > immunityTime)
        {
            hitPoint -= dmg;
            lastImmune = Time.time;
            if (hitPoint <= 0)
            {
                hitPoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}

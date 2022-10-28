using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Collidable
{
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    // TODO: discuss if we need immunity frames
    public float immunityTime = 0;
    private float lastImmune;
    
    // TODO: THERE IS TYPO WE HAVE TO CHANGE EVERYTHING XDD
    protected virtual void ReciveDamage(int dmg)
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

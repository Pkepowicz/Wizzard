using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MeleeEnemy : Fighter
{

    public int damage = 1;
    public float speed = 5.0f;

    protected override void Death()
    {
        Destroy(gameObject);
    }

    protected override void OnCollide(Collider2D coll)
    {
        // Deal damage only if target has player tag
        if (coll.tag == "Player")
        {
            coll.SendMessage("ReceiveDamage", damage);
        }
    }
}

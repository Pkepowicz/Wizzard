using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Fighter
{
    // Left there code with (*), might be usefull to make different kind of opponent
    // This code limits how much enemy can rotate between frames

    public int damage = 1;
    public float speed = 5.0f;
    // *public float step = 1.0f;
    public Transform target;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        // *transform.up = Vector3.Lerp(transform.up, target.transform.position - transform.position, step);
        // basic chasing mechanic
        transform.up = target.transform.position - transform.position;
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    protected override void Death()
    {
        Destroy(gameObject);
    }

    protected override void OnCollide(Collider2D coll)
    {
        // Deal damage only if target has player tag
        // TODO: Knokcback mechanic needed!
        if (coll.tag == "Player")
        {
            coll.SendMessage("ReciveDamage", damage);
        }
    }
}

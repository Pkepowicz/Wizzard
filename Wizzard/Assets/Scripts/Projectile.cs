using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Collidable
{
    public float speed = 20.0f;
    public float lifetime = 5f; 
    public int damage = 1;
    public float force = 0.1f;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void FixedUpdate()
    {
        // Go forward from launch offset untill you exceed life time
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (Time.time - startTime > lifetime)
            Destroy(gameObject);
    }

    protected override void OnCollide(Collider2D coll)
    {
        // Deal damage only to objects with enemy tag, and destroy itself
        if (coll.tag == "Enemy")
        {
            Destroy(gameObject);
            Damage dmg = new Damage
            {
                damageAmmount = damage,
                knockBack = force,
                origin = transform.position
            };
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}

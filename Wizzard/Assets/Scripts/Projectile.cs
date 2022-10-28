using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Collidable
{
    public float speed = 20.0f;
    public float lifetime = 5f;
    public int damage = 1;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (Time.time - startTime > lifetime)
            Destroy(gameObject);
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Enemy")
        {
            coll.SendMessage("ReciveDamage", damage);
            Destroy(gameObject);
        }
    }
}

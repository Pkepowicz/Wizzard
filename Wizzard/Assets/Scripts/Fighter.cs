using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Collidable
{
    public Rigidbody2D rb;
    public Animator anim;

    public int hitPoint = 10;
    public int maxHitPoint = 10;

    public float speed = 0.8f;
    public float maxVelocity = 1.6f;

    public float immunityTime = 0;
    private float lastImmune;

    private bool isAlive = true;

    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    protected virtual void ReceiveDamage(Damage dmg) // knockback function
    {
        if (Time.time - lastImmune > immunityTime)
        {
            hitPoint -= dmg.damageAmmount;
            rb.velocity.Set(0,0);
            rb.AddForce((transform.position - dmg.origin).normalized * dmg.knockBack, ForceMode2D.Impulse);
            lastImmune = Time.time;
            if (hitPoint <= 0)
            {
                hitPoint = 0;
                Death();
            }
            if (isAlive)
            {
                anim.SetTrigger("hit");
            }
        }
    }

    protected virtual void UpdateMotor(Vector2 moveDelta)
    {
        if (isAlive)
        {
            anim.SetFloat("moveDelta", moveDelta.x);
            if (rb.velocity.magnitude <= 1.1 * maxVelocity)
            {
                rb.AddForce(moveDelta * speed * Time.deltaTime, ForceMode2D.Force);
                rb.velocity = (Vector3.ClampMagnitude(rb.velocity, maxVelocity));
            }
        }
    }

    protected virtual void ReceiveDamage(int dmg) // to remove
    {
        if (Time.time - lastImmune > immunityTime)
        {
            hitPoint -= dmg;
            lastImmune = Time.time;
            Debug.Log("Got Damage");
            if (hitPoint <= 0)
            {
                Death();
            }
        }
    }

    protected virtual void Death()
    {
        hitPoint = 0;
        isAlive = false;
        anim.SetTrigger("death");
    }

    protected virtual void Destroy()
    {
        Destroy(gameObject);
    }
}

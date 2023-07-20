using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Collidable
{
    public Rigidbody2D rb;
    public Animator anim;
    public Transform sprite;
    private Vector3 localScale;

    [Header("Speed Settings")]
    public float speed = 0.8f;
    public float maxVelocity = 1.6f;

    [Header("Health Settings")]
    public float hitPoint = 10;
    public int maxHitPoint = 10;
    public float immunityTime = 0.2f;
    private float lastImmune = float.NegativeInfinity;

    protected bool isAlive = true;
    protected bool isImmune = false;
    public bool canMove = true;
    
    public HealthBar healthBar;


    protected virtual void Start()
    {
        // Getting components so you dont need to specify them everytime
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = transform.Find("Sprite").transform;
        localScale = sprite.transform.localScale;
        anim = gameObject.GetComponent<Animator>();
        healthBar.SetMaxHealth(maxHitPoint);
    }


    // Healing function
    public virtual void Heal(float healAmount)
    {
        if (hitPoint + healAmount > maxHitPoint)
        {
            hitPoint = maxHitPoint;
            healthBar.SetHealth(hitPoint);
        }
            
        
        else
        {
            hitPoint += healAmount;
            healthBar.SetHealth(hitPoint);
        }
            
    }
    // Movement function
    protected virtual void UpdateMotor(Vector2 moveDelta)
    {
        if (isAlive && canMove)
        {
            // changing sprite direction
            // changing sprite direction
            if (moveDelta.x > 0.15)
                sprite.transform.localScale = localScale;
            else if (moveDelta.x < -0.15)
                sprite.transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);

            anim.SetBool("moving", Mathf.Abs(moveDelta.magnitude) > 0.1);
            if (rb.velocity.magnitude <= 1.1 * maxVelocity)
            {
                rb.AddForce(moveDelta * speed * Time.deltaTime, ForceMode2D.Force);
                rb.velocity = (Vector3.ClampMagnitude(rb.velocity, maxVelocity));
            }
        }
    }

    protected IEnumerator StartImmunityPeriod(float time)
    {
        isImmune = true;
        yield return new WaitForSeconds(time);
        isImmune = false;
    }

    protected virtual void ReceiveDamage(Damage dmg) // With knockback function, return indicates if you should play hit sound or not
    {
        if (!isImmune)
        {
            hitPoint -= dmg.damageAmmount;
            rb.velocity.Set(0,0);
            rb.AddForce((transform.position - dmg.origin).normalized * dmg.knockBack, ForceMode2D.Impulse);
            StartCoroutine(StartImmunityPeriod(immunityTime));
            if (hitPoint <= 0)
            {
                hitPoint = 0;
                Death();
            }
            healthBar.SetHealth(hitPoint);
            if (isAlive && dmg.damageAmmount > 0)
            {
                anim.SetTrigger("hit");
                HitSound();
            }
        }
    }



    // dot damage can bypass immunity frames
    protected virtual void ReceiveDamage(float dmg) // don't delete, damage over time effects use this function
    {
        hitPoint -= dmg;
        if (hitPoint <= 0)
        {
            Death();
        }
        healthBar.SetHealth(hitPoint);
        if (isAlive)
        {
            anim.SetTrigger("hit");

        }
    }

    protected virtual void TakeDamageFromDot(float damage) // damage taken from dots should bypass immunity frames
    {
        hitPoint -= damage;
        healthBar.SetHealth(hitPoint);
        if (hitPoint <= 0)
        {
            Death();
        }
    }

    // Destruction of object is handled by animation
    // Cause of that we need 2 separate functions for death and destruction
    protected virtual void Death()
    {
        hitPoint = 0;
        isAlive = false;
        DeathSound();
        anim.SetTrigger("death");
    }


    protected virtual void Destroy()
    {
        Destroy(gameObject);
    }
    
    protected virtual void HitSound()
    {
        SoundManager.PlaySound("EnemyTakeDamage", transform.position);
    }

    protected virtual void DeathSound()
    {
        SoundManager.PlaySound("EnemyDeath", transform.position);
    }
}

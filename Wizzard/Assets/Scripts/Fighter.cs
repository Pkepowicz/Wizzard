using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Collidable
{
    public Rigidbody2D rb;
    public Animator anim;

    [Header("Speed Settings")]
    public float speed = 0.8f;
    public float maxVelocity = 1.6f;

    [Header("Health Settings")]
    public float hitPoint = 10;
    public int maxHitPoint = 10;
    public float immunityTime = 0.2f;
    private float lastImmune = float.NegativeInfinity;

    private bool isAlive = true;
    private bool isImmune = false;


    protected virtual void Start()
    {
        // Getting components so you dont need to specify them everytime
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

    }

    // Healing function
    public virtual void Heal(float healAmount)
    {
        if (hitPoint + healAmount > maxHitPoint)
            hitPoint = maxHitPoint;
        else
            hitPoint += healAmount;
    }
    // Movement function
    protected virtual void UpdateMotor(Vector2 moveDelta)
    {
        if (isAlive)
        {
            anim.SetFloat("moveDelta", moveDelta.x);
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

    protected virtual void ReceiveDamage(Damage dmg) // With knockback function
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
            if (isAlive && dmg.damageAmmount > 0)
            {
                anim.SetTrigger("hit");
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
        if (isAlive)
        {
            anim.SetTrigger("hit");

        }
    }

    protected virtual void TakeDamageFromDot(float damage) // damage taken from dots should bypass immunity frames
    {
        hitPoint -= damage;
        Debug.Log("Got Damage");
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
        anim.SetTrigger("death");
    }


    protected virtual void Destroy()
    {
        Destroy(gameObject);
    }
}

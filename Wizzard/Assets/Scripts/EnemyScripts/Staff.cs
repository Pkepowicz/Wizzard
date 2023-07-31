using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    public EnemyAI enemyAI;
    public Vector3 originalSize;
    public Animator anim;
    public GameObject attackPrefab;
    public float attackRange = 1f;
    public float attackSpeed = 3f;
    private bool canAttack = true;

    private static bool onCooldown = false;

    protected void Start()
    {
        originalSize = transform.localScale;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if ((transform.position - enemyAI.targetPosition.position).magnitude <= attackRange && canAttack && !onCooldown)
        {
            StartCoroutine(Attack());
        }

    }
    
    protected IEnumerator Attack()
    {
        enemyAI.canMove = false;
        canAttack = false;
        anim.SetTrigger("attack");
        StartCoroutine(StartSharedCooldown(0.7f));
        yield return new WaitForSeconds(attackSpeed);
        enemyAI.canMove = true;
        canAttack = true;
    }

    protected IEnumerator StartSharedCooldown(float cooldown)
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    protected void spawnExplosion()
    {
        Instantiate(attackPrefab, enemyAI.targetPosition.position, Quaternion.identity);
        enemyAI.canMove = true;
    }

    protected void AttackSound()
    {
        SoundManager.PlaySound("SatyrAttack", transform.position);
    }
}

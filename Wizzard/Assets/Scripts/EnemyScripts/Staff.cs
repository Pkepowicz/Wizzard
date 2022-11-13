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
    public float attackSpeed = 2f;
    private bool canAttack = true;

    protected void Start()
    {
        originalSize = transform.localScale;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if ((transform.position - enemyAI.targetPosition.position).magnitude <= attackRange && canAttack)
        {
            StartCoroutine(Attack());
        }

    }
    
    protected IEnumerator Attack()
    {
        enemyAI.canMove = false;
        canAttack = false;
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(attackSpeed);
        enemyAI.canMove = true;
        canAttack = true;
    }

    protected void spawnExplosion()
    {
        Instantiate(attackPrefab, enemyAI.targetPosition.position, Quaternion.identity);
        enemyAI.canMove = true;
    }
}

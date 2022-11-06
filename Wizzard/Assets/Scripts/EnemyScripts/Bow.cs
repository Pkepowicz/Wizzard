using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public EnemyAI enemyAi;
    public Animator anim;
    public GameObject arrow;
    public Transform offset;
    public float startAttackRange;
    public float attackRange;
    public float attackSpeed;
    private float lastAttack = float.NegativeInfinity;

    private void Start()
    {
        enemyAi = GetComponentInParent<EnemyAI>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // first checks if is in range then checks attack speed
        if ((enemyAi.transform.position - enemyAi.targetPosition.transform.position).magnitude <= startAttackRange && (Time.time - lastAttack) > attackSpeed)
        {
            enemyAi.canMove = false;
            anim.SetTrigger("Draw");
            lastAttack = Time.time;
        }
    }

    public void shoot()
    {
        // checks if target didnt move out of max range if he did, cancels attack, function called by anim
        if ((enemyAi.transform.position - enemyAi.targetPosition.transform.position).magnitude <= attackRange)
        {
            Instantiate(arrow, offset.position, enemyAi.hand.transform.rotation);
        }
        enemyAi.canMove = true;
    }
}

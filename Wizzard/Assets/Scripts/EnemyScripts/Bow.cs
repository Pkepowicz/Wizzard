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

    private void Start()
    {
        enemyAi = GetComponentInParent<EnemyAI>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if ((enemyAi.transform.position - enemyAi.targetPosition.transform.position).magnitude <= startAttackRange)
        {
            enemyAi.canMove = false;
            anim.SetTrigger("Draw");
        }
    }

    public void shoot()
    {
        if ((enemyAi.transform.position - enemyAi.targetPosition.transform.position).magnitude <= attackRange)
        {
            Instantiate(arrow, offset.position, enemyAi.hand.transform.rotation); // spagetthi :))
            //anim.SetTrigger("Shoot");
            enemyAi.canMove = true;
        }
        else
        {
            //anim.SetTrigger("OutOfRange");
            enemyAi.canMove = true;
        }
    }
}

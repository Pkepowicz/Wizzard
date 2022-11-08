using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public EnemyAI enemyAi;
    public Animator anim;
    public float attackSpeed;
    public float attackRange;
    public float chargeForce;
    private float lastAttack = float.NegativeInfinity;

    private void Start()
    {
        enemyAi = GetComponentInParent<EnemyAI>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if ((enemyAi.transform.position - enemyAi.targetPosition.transform.position).magnitude <= attackRange && (Time.time - lastAttack) > attackSpeed)
        {
            enemyAi.canMove = false;
            anim.SetTrigger("Charge");
            lastAttack = Time.time;
        }
    }

    public void addForce()
    {
        enemyAi.rb.AddForce((enemyAi.targetPosition.transform.position - enemyAi.transform.position).normalized * chargeForce);
        enemyAi.canMove = true;
    }
}

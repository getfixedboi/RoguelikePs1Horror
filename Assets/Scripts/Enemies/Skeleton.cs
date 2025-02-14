using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : BaseEnemy
{
    private bool isAttacking = false;
    private bool isTakingDamage=false;

    public override void Awake()
    {
        base.Awake();
    }

    public void Update()
    {
        if (isAttacking){ return; }
        if (isDead) { return; }
        if(isTakingDamage) { return; }

        float dist = Vector3.Distance(transform.position, target.position);

        if (dist>agent.stoppingDistance)
		{
			agent.SetDestination(target.position);
		}
        else
        {
            agent.SetDestination(transform.position);
        }

        if (agent.remainingDistance >= agent.stoppingDistance && !agent.pathPending)
        {
            anim.Play("walk");
        }
        else if (!target)
        {
            anim.Play("idle");
        }


        Vector3 direction = target.position - gameObject.transform.position;

        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, direction, out hit, agent.stoppingDistance + 1f))
        {
            if (hit.collider.gameObject.GetComponent<PlayerStatictics>())
            {
                Attack();
                hit.collider.gameObject.GetComponent<PlayerStatictics>().TakeDamage(dealDamage);
            }
        }
    }

    private void Attack() => StartCoroutine(C_Attack());

    private IEnumerator C_Attack()
    {
        isAttacking = true;
        agent.SetDestination(transform.position);
        anim.Play("attack");
        yield return new WaitForSeconds(3.3f);
        isAttacking = false;
    }
    public override void TakeDamage(int damage)=>StartCoroutine(C_TakeDamage(damage));

    private IEnumerator C_TakeDamage(int damage)
    {
        isTakingDamage=true;
        currentHp -= damage;
        if (currentHp <= 0)
        {
            isDead = true; 
        }

        if (isDead)
        {
            anim.Play("death");
            yield return new WaitForSeconds(1.85f);
            Destroy(gameObject,0);
        }
        else
        {
            anim.Play("take damage");
            yield return new WaitForSeconds(2.8f);
        }
        isTakingDamage=false;
    }
}
